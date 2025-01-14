using YouTube.Domain.Common;

namespace YouTube.Domain.Entities;

/// <summary>
/// Плейлист
/// </summary>
public class Playlist : BaseEntity
{
    /// <summary>
    /// Скрыт ли он
    /// </summary>
    public bool IsHidden { get; set; }

    /// <summary>
    /// Название
    /// </summary>
    public string Name { get; set; } = default!;

    /// <summary>
    /// Описание
    /// </summary>
    public string? Description { get; set; }
    
    /// <summary>
    /// Дата создания
    /// </summary>
    public DateOnly CreateDate { get; set; }

    /// <summary>
    /// Видосы в плейлисте
    /// </summary>
    public ICollection<Video>? Videos { get; set; } = default!;
    
    /// <summary>
    /// Id Канала
    /// </summary>
    public Guid ChannelId { get; set; }

    /// <summary>
    /// Канал
    /// </summary>
    public Channel Channel { get; set; } = default!;
}