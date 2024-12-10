using Microsoft.AspNetCore.SignalR;
using YouTube.Application.Common.Messages.Error;
using YouTube.Application.Common.Requests.Chats;
using YouTube.Application.DTOs.Chat;
using YouTube.Application.Interfaces;

namespace YouTube.Infrastructure.Hubs;

/// <summary>
/// Хаб для чата тех поддержки
/// </summary>
public class SupportChatHub : Hub
{
    private readonly IChatService _chatService;

    public SupportChatHub(IChatService chatService)
    {
        _chatService = chatService;
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
        if (request.Message == null)
            throw new ArgumentException(ChatErrorMessage.MessageIsEmpty);

        await _chatService.SendMessageAsync(request);
    }
    
    /// <summary>
    /// Прочитать сообщение
    /// </summary>
    /// <param name="request">Список ид сообщений, ид чата</param>
    public async Task ReadMessages(ReadMessagesRequest request)
    {
        if (request.MessagesId.Count == 0 || request.MessagesId is null)
            throw new ArgumentException(ChatErrorMessage.MessagesIsEmpty);

        await _chatService.ReadMessagesAsync(request.MessagesId);
        
        await Clients.Group(request.ChatId.ToString()).SendAsync("ReadMessages", new
        {
            request.ChatId,
            request.MessagesId
        });
    }

    /// <summary>
    /// Выйти из чата
    /// </summary>
    /// <param name="chatId">Id чата</param>
    public async Task LeaveChat(Guid chatId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatId.ToString());
    }
}