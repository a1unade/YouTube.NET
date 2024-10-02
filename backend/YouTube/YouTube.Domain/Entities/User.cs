using Microsoft.AspNetCore.Identity;

namespace YouTube.Domain.Entities;

public class User : IdentityUser<Guid>
{
    /// <summary>
    /// Подписка
    /// </summary>
    public Premium? Subscriptions { get; set; }

    /// <summary>
    /// Каналы
    /// </summary>
    public ICollection<Channel>? Channels { get; set; }

    /// <summary>
    /// Транзакции
    /// </summary>
    public ICollection<Transaction>? Transactions { get; set; } 
    
    /// <summary>
    /// nav-prop Файл
    /// </summary>
    public Guid? AvatarId { get; set; }

    /// <summary>
    /// Аватаркa
    /// </summary>
    public File? AvatarUrl { get; set; } 
    
    /// <summary>
    /// Дополнительная Информация о пользователе
    /// </summary>
    public UserInfo UserInfo { get; set; } = default!;
}
