using YouTube.Domain.Common;

namespace YouTube.Domain.Entities;

public class UserInfo : BaseEntity
{
    /// <summary>
    /// Имя пользователя
    /// </summary>
    public required string Name { get; set; } 

    /// <summary>
    /// Фамилия пользователя
    /// </summary>
    public required string Surname { get; set; }

    /// <summary>
    /// Дата рождения пользователя
    /// </summary>
    public DateOnly BirthDate { get; set; }

    /// <summary>
    /// Пол пользователя
    /// </summary>
    public string Gender { get; set; } = default!;
    
    /// <summary>
    /// Страна
    /// </summary>
    public string? Country { get; set; }
    
    /// <summary>
    /// Id Пользователя
    /// </summary>
    public Guid UserId { get; set; }
}