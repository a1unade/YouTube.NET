using YouTube.Domain.Common;

namespace YouTube.Domain.Entities;

/// <summary>
/// Ссылки на соц-сети
/// </summary>
public class Link : BaseEntity
{
    /// <summary>
    /// Ссылка
    /// </summary>
    public string Reference { get; set; } = default!;

    /// <summary>
    /// Ид канала
    /// </summary>
    public Guid ChannelId { get; set; }
    
    /// <summary>
    /// Канал
    /// </summary>
    public Channel Channel { get; set; } = default!;
}