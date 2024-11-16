using YouTube.Domain.Entities;

namespace YouTube.Application.Interfaces.Repositories;

/// <summary>
/// Репозиторий для работы с каналом
/// </summary>
public interface IChannelRepository
{
    /// <summary>
    /// Получить канал с его файлами
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Channel?> GetByIdWithImg(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Получить кол-во видео канала
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public int GetChannelVideoCount(Guid id);

    /// <summary>
    /// Получить канал с его данными
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Channel?> GetChannelWithLinks(Guid id, CancellationToken cancellationToken);
}