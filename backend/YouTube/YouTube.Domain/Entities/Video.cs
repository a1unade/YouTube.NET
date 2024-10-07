using YouTube.Domain.Common;

namespace YouTube.Domain.Entities;
/// <summary>
/// Видео
/// </summary>
public class Video : BaseEntity
{
    /// <summary>
    /// Название
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Описание 
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Кол-во просмотров
    /// </summary>
    public int ViewCount { get; set; }

    /// <summary>
    /// Кол-во Лайков
    /// </summary>
    public int LikeCount { get; set; }

    /// <summary>
    /// Кол-во Дизлайков
    /// </summary>
    public int DisLikeCount { get; set; }

    /// <summary>
    /// Дата создания
    /// </summary>
    public DateOnly ReleaseDate { get; set; }
    
    /// <summary>
    /// Страна
    /// </summary>
    public string? Country { get; set; }
    
    /// <summary>
    /// ID канала
    /// </summary>
    public Guid ChannelId { get; set; }
    
    /// <summary>
    /// Скрытое ли?
    /// </summary>
    public bool IsHidden { get; set; }
    
    /// <summary>
    /// Ограничение по возрасту
    /// </summary>
    public sbyte Age { get; set; }

    /// <summary>
    /// Канал
    /// </summary>
    public Channel Channel { get; set; } = default!;

    /// <summary>
    /// Комментарии к видео
    /// </summary>
    public ICollection<Comment>? Comments { get; set; }
    
    /// <summary>
    /// Ид превью
    /// </summary>
    public Guid PreviewImgId { get; set; }
    
    /// <summary>
    /// Путь к превью
    /// </summary>
    public File PreviewImg { get; set; } = default!;
    
    /// <summary>
    /// Ид видео
    /// </summary>
    public Guid VideoUrlId { get; set; }

    /// <summary>
    /// Путь до видео
    /// </summary>
    public File VideoUrl { get; set; } = default!;

    /// <summary>
    /// Ид категории
    /// </summary>
    public Guid? CategoryId { get; set; }
    
    /// <summary>
    /// Категории
    /// </summary>
    public Category? Category { get; set; }

    /// <summary>
    /// Плейлисты
    /// </summary>
    public ICollection<Playlist>? Playlists { get; set; }
}