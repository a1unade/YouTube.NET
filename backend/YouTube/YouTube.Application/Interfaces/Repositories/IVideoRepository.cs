using YouTube.Domain.Entities;

namespace YouTube.Application.Interfaces.Repositories;
/// <summary>
/// Репозиторий для работы с видео
/// </summary>
public interface IVideoRepository
{
    /// <summary>
    /// Получить список видео
    /// </summary>
    /// <param name="page">Страница</param>
    /// <param name="size">Размер страницы</param>
    /// <param name="category">Категория</param>
    /// <param name="sort">Сортировка</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Список видео</returns>
    Task<List<Video>> GetVideoPagination(int page, int size, string? category, string? sort, CancellationToken cancellationToken);

    /// <summary>
    /// Получить видео по их id
    /// </summary>
    /// <param name="guids"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<List<Video>> GetVideosByIds(List<Guid> guids, CancellationToken cancellationToken);
}