using YouTube.Domain.Common;

namespace YouTube.Domain.Entities;

public class Transaction : BaseEntity
{
    /// <summary>
    /// Дата покупки
    /// </summary>
    public DateTime Date { get; set; }
    
    /// <summary>
    /// Цена
    /// </summary>
    public decimal Price { get; set; }
    
    /// <summary>
    /// Описание
    /// </summary>
    public string? Description { get; set; }
    
    /// <summary>
    /// Операция 
    /// </summary>
    public required string Operation { get; set; }
    
    /// <summary>
    /// Id Пользователя
    /// </summary>
    public Guid UserId { get; set; }
    
    /// <summary>
    /// Пользователь
    /// </summary>
    public User User { get; set; } = default!;
}