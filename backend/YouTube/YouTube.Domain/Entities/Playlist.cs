using YouTube.Domain.Common;

namespace YouTube.Domain.Entities;

/// <summary>
/// Плейлист
/// </summary>
public class Playlist : BaseEntity
{
    /// <summary>
    /// Видно ли его?
    /// </summary>
    public bool IsHidden { get; set; }

    /// <summary>
    /// Видосы в плейлисте
    /// </summary>
    public ICollection<Video> Videos { get; set; } = default!;
    
    /// <summary>
    /// Id Канала
    /// </summary>
    public Guid ChannelId { get; set; }

    /// <summary>
    /// Канал
    /// </summary>
    public Channel Channel { get; set; } = default!;
}