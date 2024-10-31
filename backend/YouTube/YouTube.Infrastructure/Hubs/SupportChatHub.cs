using MassTransit;
using Microsoft.AspNetCore.SignalR;
using YouTube.Application.Common.Messages.Error;
using YouTube.Application.DTOs.Chat;
using YouTube.Application.Interfaces;

namespace YouTube.Infrastructure.Hubs;

/// <summary>
/// Хаб для чата тех поддержки
/// </summary>
public class SupportChatHub : Hub
{
    private readonly IChatService _chatService;
    private readonly IBus _bus;

    public SupportChatHub(IChatService chatService, IBus bus)
    {
        _chatService = chatService;
        _bus = bus;
    }

    /// <summary>
    /// Зайти в чат
    /// </summary>
    /// <param name="connection">Id Пользователя и чата</param>
    public async Task<Guid?> JoinChat(ChatConnection connection)
    {
        if(connection.ChatId is null)
            connection.ChatId = await _chatService.CreateChatAsync(connection.UserId);
        
        await Groups.AddToGroupAsync(Context.ConnectionId, connection.ChatId.ToString()!);
        return connection.ChatId;
    }

    /// <summary>
    /// Отправить сообщение
    /// </summary>
    /// <param name="request">Данные</param>
    public async Task SendMessage(SendMessageRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Message))
            throw new ArgumentException(ChatErrorMessage.MessageIsEmpty);
        
        await Clients.Group(request.ChatId.ToString()).SendAsync("ReceiveMessage", new
        {
            request.UserId,
            request.ChatId,
            request.Message
        });
        
        await _bus.Publish(request);
    }

    /// <summary>
    /// Выйти из чата
    /// </summary>
    /// <param name="chatId">Id отправителя</param>
    public async Task LeaveChat(Guid chatId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatId.ToString());
    }
}