using YouTube.Domain.Common;

namespace YouTube.Domain.Entities;

public class UserInfo : BaseEntity
{
    /// <summary>
    /// ID пользователя
    /// </summary>
    public Guid UserId { get; set; }
        
    /// <summary>
    /// Никнейм
    /// </summary>
    public string Nickname { get; set; }

    /// <summary>
    /// Навигационное свойство для связи с User
    /// </summary>
    public User User { get; set; }
}