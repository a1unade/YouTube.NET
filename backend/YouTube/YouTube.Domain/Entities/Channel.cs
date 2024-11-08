using System.Text.Json.Serialization;
using YouTube.Domain.Common;

namespace YouTube.Domain.Entities;

/// <summary>
/// Канал
/// </summary>
public class Channel : BaseEntity
{
    /// <summary>
    /// Имя
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Описание
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Дата создания
    /// </summary>
    public DateOnly CreateDate { get; set; }

    /// <summary>
    /// Кол-во подписчиков
    /// </summary>
    public int SubCount { get; set; }

    /// <summary>
    /// Id пользователя
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Пользователь
    /// </summary>
    [JsonIgnore]
    public User User { get; set; } = default!;

    /// <summary>
    /// Id Аватарки
    /// </summary>
    public Guid? MainImgId { get; set; }

    /// <summary>
    /// Файл аватарки
    /// </summary>
    public File? MainImgFile { get; set; } 

    /// <summary>
    /// Id банера
    /// </summary>
    public Guid? BannerImgId { get; set; }

    /// <summary>
    /// Банер
    /// </summary>
    public File? BannerImg { get; set; }

    /// <summary>
    /// Страна
    /// </summary>
    public string? Country { get; set; } 

    /// <summary>
    /// Подписки на каналы
    /// </summary>
    public ICollection<ChannelSubscription> UserChannelSubs { get; set; } = default!;

    /// <summary>
    /// Список видео
    /// </summary>
    public ICollection<Video> Videos { get; set; } = default!;

    /// <summary>
    /// Список комментариев
    /// </summary>
    public ICollection<Comment> Comments { get; set; } = default!;
    
    /// <summary>
    /// Плейлисты
    /// </summary>
    public ICollection<Playlist> Playlists { get; set; } = default!;

    /// <summary>
    /// Подписки
    /// </summary>
    public ICollection<ChannelSubscription> Subscriptions { get; set; } = default!;
    
    /// <summary>
    /// Подписчики
    /// </summary>
    public ICollection<ChannelSubscription> Subscribers { get; set; } = default!;

    /// <summary>
    /// Список ссылок
    /// </summary>
    public ICollection<Link> Links { get; set; } = default!;
}