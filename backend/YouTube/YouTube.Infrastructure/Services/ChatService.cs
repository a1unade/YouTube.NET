using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using YouTube.Application.Common.Exceptions;
using YouTube.Application.Common.Messages.Error;
using YouTube.Application.Common.Requests.Chats;
using YouTube.Application.DTOs.Chat;
using YouTube.Application.Interfaces;
using YouTube.Domain.Entities;
using YouTube.Infrastructure.Hubs;

namespace YouTube.Infrastructure.Services;

public class ChatService : IChatService
{
    private readonly IDbContext _context;
    private readonly UserManager<User> _userManager;
    private readonly IHubContext<SupportChatHub> _hubContext;
    private readonly IBus _bus;

    public ChatService(IDbContext context, UserManager<User> userManager,
        IHubContext<SupportChatHub> hubContext, IBus bus)
    {
        _context = context;
        _userManager = userManager;
        _hubContext = hubContext;
        _bus = bus;
    }

    public async Task<Guid> CreateChatAsync(Guid userId)
    {
        var user = await _context.Users
                       .Include(u => u.ChatHistory)
                       .FirstOrDefaultAsync(u => u.Id == userId)
                   ?? throw new NotFoundException(UserErrorMessage.UserNotFound);

        if (!await _userManager.IsInRoleAsync(user, "User"))
            return Guid.Empty;

        if (user.ChatHistory != null!)
            return user.ChatHistory.Id;

        var chatHistory = new ChatHistory
        {
            StartDate = DateOnly.FromDateTime(DateTime.Now),
            UserId = user.Id,
            ChatMessages = new List<ChatMessage>()
        };

        _context.ChatHistories.Add(chatHistory);
        await _context.SaveChangesAsync();
        return chatHistory.Id;
    }

    public async Task<Guid> SendMessageAsync(SendMessageRequest request)
    {
        var messageRequest = new MessageRequest
        {
            ChatId = request.ChatId,
            UserId = request.UserId,
            MessageId = Guid.NewGuid(),
            Message = request.Message,
            FileId = request.FileId
        };

        await _bus.Publish(messageRequest);

        var file = await _context.Files.FirstOrDefaultAsync(x => x.Id == request.FileId);

        await _hubContext.Clients.Group(request.ChatId.ToString())
            .SendAsync("ReceiveMessage", new
            {
                MessageId = messageRequest.MessageId,
                UserId = messageRequest.UserId,
                ChatId = messageRequest.ChatId,
                Message = messageRequest.Message,
                Attachment = new { messageRequest.FileId, file?.ContentType },
                Date = DateOnly.FromDateTime(DateTime.Now),
                Time = TimeOnly.FromDateTime(DateTime.Now)
            });

        return messageRequest.MessageId;
    }
    
    public async Task<Guid> SendMessageGrpcAsync(SendMessageRequest request)
    {
        var messageRequest = new MessageRequest
        {
            ChatId = request.ChatId,
            UserId = request.UserId,
            MessageId = Guid.NewGuid(),
            Message = request.Message,
            FileId = request.FileId
        };

        await _bus.Publish(messageRequest);
        
        return messageRequest.MessageId;
    }

    public async Task ReadMessagesAsync(List<Guid> messages)
        => await _context.ChatMessages
            .Where(x => messages.Contains(x.Id))
            .ExecuteUpdateAsync(x => x.SetProperty(r => r.IsRead, true));
}