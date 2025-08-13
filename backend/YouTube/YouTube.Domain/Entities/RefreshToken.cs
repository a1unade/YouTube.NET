using YouTube.Domain.Common;

namespace YouTube.Domain.Entities;

/// <summary>
/// Рефреш токен
/// </summary>
public class RefreshToken : BaseEntity
{
    /// <summary>
    /// Токен
    /// </summary>
    public string Token { get; set; } = null!; 

    /// <summary>
    /// Дата и время истечения срока действия токена.
    /// После этой даты токен автоматически становится недействительным.
    /// </summary>
    public DateTime Expires { get; set; }
    
    /// <summary>
    /// Время создания 
    /// </summary>
    public DateTime Created { get; set; }

    /// <summary>
    /// Дата и время отзыва токена
    /// </summary>
    public DateTime? Revoked { get; set; }

    /// <summary>
    /// Токен, который заменил текущий (если null — не был заменён)
    /// </summary>
    public string? ReplacedByToken { get; set; } 
    
    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public Guid UserId { get; set; }
    
    /// <summary>
    /// Пользователь
    /// </summary>
    public User User { get; set; } = null!;
    
      
    /// <summary>
    /// Проверяет, истёк ли срок действия токена
    /// </summary>
    public bool IsExpired => DateTime.UtcNow >= Expires;
    
    /// <summary>
    /// Проверяет, отозван ли токен
    /// </summary>
    public bool IsRevoked => Revoked != null;
    
    /// <summary>
    /// Проверяет, активен ли токен (не отозван и не истёк).
    /// </summary>
    public bool IsActive => !IsRevoked && !IsExpired;
}