using YouTube.Domain.Common;

namespace YouTube.Domain.Entities;

/// <summary>
/// Пост
/// </summary>
public class Post : BaseEntity
{
    /// <summary>
    /// Описание
    /// </summary>
    public required string Description { get; set; }
    
    /// <summary>
    /// Дата
    /// </summary>
    public DateOnly Date { get; set; }

    /// <summary>
    /// Пользователь
    /// </summary>
    public User User { get; set; } = default!;
    
    /// <summary>
    /// ID Пользователя
    /// </summary>
    public Guid UserId { get; set; }
    
    /// <summary>
    /// Кол-во лайков
    /// </summary>
    public int LikeCount { get; set; }
    
    /// <summary>
    /// Кол-во дизлайков
    /// </summary>
    public int DislikeCount { get; set; }
    
    /// <summary>
    /// Комментарии
    /// </summary>
    public int CommentCount { get; set; }

    /// <summary>
    /// Комментарии
    /// </summary>
    public ICollection<Comment> Comments { get; set; } = default!;
}