using YouTube.Domain.Entities;

namespace YouTube.Application.Interfaces;

/// <summary>
/// Сервис для работы с плейлистами
/// </summary>
public interface IPlaylistService
{
    /// <summary>
    /// Создать дефолтные плейлисты
    /// </summary>
    /// <param name="channel">Канал</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns></returns>
    Task CreateDefaultPlaylists(Channel channel, CancellationToken cancellationToken);
}