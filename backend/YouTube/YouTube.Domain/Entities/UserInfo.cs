using YouTube.Domain.Common;

namespace YouTube.Domain.Entities;

public class UserInfo : BaseEntity
{
    /// <summary>
    /// ID пользователя
    /// </summary>
    public Guid UserId { get; set; }
        
    /// <summary>
    /// Имя пользователя
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Фамилия пользователя
    /// </summary>
    public string? Surname { get; set; }
    
    /// <summary>
    /// Дата рождения пользователя
    /// </summary>
    public DateTime BirthDate { get; set; }
    
    public string? Country { get; set; }
    
    /// <summary>
    /// Пол пользователя
    /// </summary>
    public string Gender { get; set; }
    
    public bool Premium { get; set; }

    /// <summary>
    /// Навигационное свойство для связи с User
    /// </summary>
    public User User { get; set; }
    
    
    public Channel Channel { get; set; }
    
    public List<Comment>? Comments { get; set; }
    
    public List<UserChannelSub> ChannelSubs { get; set; }
    
    /// <summary>
    /// ID аватарки
    /// </summary>
    public int? AvatarId { get; set; }
    
    public StaticFile StaticFile { get; set; }
}