using YouTube.Domain.Common;

namespace YouTube.Domain.Entities;

/// <summary>
/// Комментарии
/// </summary>
public class Comment : BaseEntity
{
    /// <summary>
    /// Комментарий
    /// </summary>
    public required string CommentText { get; set; }

    /// <summary>
    /// Дата поста комментария
    /// </summary>
    public DateOnly PostDate { get; set; }

    /// <summary>
    /// Кол-во лайков
    /// </summary>
    public int LikeCount { get; set; }

    /// <summary>
    /// Кол-во дизлайков
    /// </summary>
    public int DislikeCount { get; set; }

    /// <summary>
    /// ID Видео
    /// </summary>
    public Guid VideoId { get; set; }

    /// <summary>
    /// Видео
    /// </summary>
    public Video Video { get; set; } = default!;

    /// <summary>
    /// Id Канала
    /// </summary>
    public Guid ChannelId { get; set; }

    /// <summary>
    /// Канал оставивший комментарий
    /// </summary>
    public Channel Channel { get; set; } = default!;
    
    public Guid PostId { get; set; }

    public Post Post { get; set; } = default!;
}