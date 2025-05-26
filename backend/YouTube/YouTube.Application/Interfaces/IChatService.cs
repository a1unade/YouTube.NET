using MassTransit;
using YouTube.Application.Common.Requests.Chats;

namespace YouTube.Application.Interfaces;
/// <summary>
/// Сервис для работы с чатом
/// </summary>
public interface IChatService
{
    /// <summary>
    /// Создать чат для пользователя
    /// </summary>
    /// <param name="userId">Id пользователя</param>
    /// <returns></returns>
    public Task<Guid> CreateChatAsync(Guid userId);

    /// <summary>
    /// Прочитать сообщения 
    /// </summary>
    /// <param name="messages">Список id сообщений</param>
    /// <returns></returns>
    public Task ReadMessagesAsync(List<Guid> messages);

    /// <summary>
    /// Добавить сообщение
    /// </summary>
    /// <param name="request">запрос с сообщением</param>
    public Task<Guid> SendMessageAsync(SendMessageRequest request);
    
    
    /// <summary>
    /// Добавить сообщение через Grpc
    /// </summary>
    /// <param name="request">запрос с сообщением</param>
    public Task<Guid> SendMessageGrpcAsync(SendMessageRequest request);
}