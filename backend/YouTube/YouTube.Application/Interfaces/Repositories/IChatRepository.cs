using YouTube.Domain.Entities;

namespace YouTube.Application.Interfaces.Repositories;

/// <summary>
/// Репозиторий для работы с чатами
/// </summary>
public interface IChatRepository
{
    /// <summary>
    /// Получить историю по Id
    /// </summary>
    /// <param name="id">Id</param>
    /// <param name="cancellationToken">токен отмены</param>
    /// <returns>История чата</returns>
    Task<ChatHistory?> GetChatHistoryById(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Получить последнее сообщение 
    /// </summary>
    /// <param name="page">Страница</param>
    /// <param name="size">Размер</param>
    /// <param name="cancellationToken">токен отмены</param>
    /// <returns>Сообщение</returns>
    Task<List<ChatHistory>> GetChatHistoryPagination(int page, int size, CancellationToken cancellationToken);

    /// <summary>
    /// Пагинация по дням 
    /// </summary>
    /// <param name="id">История чата id</param>
    /// <param name="page">Страница</param>
    /// <param name="cancellationToken">токен отмены</param>
    /// <returns>Коллекция сообщений</returns>
    Task<List<ChatMessage>> GetChatMessagesPagination(Guid id, int page, CancellationToken cancellationToken);
}