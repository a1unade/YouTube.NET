namespace YouTube.Application.Interfaces;
/// <summary>
/// Сервис для работы с чатом
/// </summary>
public interface IChatService
{
    /// <summary>
    /// Создать чат для польователя
    /// </summary>
    /// <param name="userId">Id пользователя</param>
    /// <returns></returns>
    public Task<Guid> CreateChatAsync(Guid userId);
}