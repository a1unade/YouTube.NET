using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.IdentityModel.Tokens;
using YouTube.Application.DTOs.Chat;
using YouTube.Application.DTOs.Video;

namespace YouTube.Infrastructure.Hubs;

public class SupportChatHub : Hub
{
    private readonly IPublishEndpoint _publishEndpoint;

    public SupportChatHub(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    public async Task JoinChat(Guid userId) => await Groups.AddToGroupAsync(Context.ConnectionId, userId.ToString());

    public async Task SendMessage(Guid userId, string message)
    {
        await Clients.Group(userId.ToString()).SendAsync("ReceiveMessage", userId, message);

        await _publishEndpoint.Publish(new MessageInfo
        {
            UserId = userId,
            Message = message
        });
    }

    public async Task SendFile(Guid userId, IFormFile file)
    {
        await Clients.Group(userId.ToString()).SendAsync("ReceiveMessage", userId, file);

        byte[] fileBytes;
        using (var memoryStream = new MemoryStream())
        {
            await file.CopyToAsync(memoryStream);
            fileBytes = memoryStream.ToArray();
        }
        
        await _publishEndpoint.Publish(new MessageInfo
        {
            UserId = userId,
            FileContent = new FileContent
            {
                Bytes = fileBytes,
                FileName = file.FileName,
                ContentType = file.ContentType,
                Lenght = file.Length,
                Bucket = userId.ToString()
            }
        });
    }

    public async Task SendAll(Guid userId, string? message, IFormFile? file)
    {
        if (message.IsNullOrEmpty() && file is null)
            throw new ArgumentException();
        
        var messageInfo = new MessageInfo { UserId = userId };
        if (file is not null)
        {
            byte[] fileBytes;
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                fileBytes = memoryStream.ToArray();
            }

            messageInfo.FileContent = new FileContent
            {
                FileName = file.FileName,
                ContentType = file.ContentType,
                Lenght = file.Length,
                Bucket = userId.ToString(),
                Bytes = fileBytes
            };
        }

        if (!string.IsNullOrWhiteSpace(message))
            messageInfo.Message = message;
        
        
        await Clients.Group(userId.ToString()).SendAsync("ReceiveMessage", messageInfo);
        
        await _publishEndpoint.Publish(messageInfo);
    }

    public async Task LeaveChat(Guid userId) => await Groups.RemoveFromGroupAsync(Context.ConnectionId, userId.ToString());
}