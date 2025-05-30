using YouTube.Domain.Common;

namespace YouTube.Domain.Entities;

/// <summary>
/// Премиум подписка
/// </summary>
public class Premium : BaseEntity
{
    /// <summary>
    /// Начало подписки
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Конец подписки
    /// </summary>
    public DateTime? EndDate { get; set; }

    /// <summary>
    /// Цена
    /// </summary>
    public decimal Price { get; set; }
    
    /// <summary>
    /// Активна?
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// Id пользователя
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Юзер
    /// </summary>
    public User User { get; set; } = default!;
    
    public Premium Clone()
    {
        return new Premium
        {
            Id = this.Id,
            StartDate = this.StartDate,
            EndDate = this.EndDate,
            Price = this.Price,
            IsActive = this.IsActive,
            UserId = this.UserId
        };
    }
}