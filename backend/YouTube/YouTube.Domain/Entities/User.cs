using Microsoft.AspNetCore.Identity;

namespace YouTube.Domain.Entities;

public class User : IdentityUser<Guid>
{
    public Guid UserInfoId { get; set; }
    
    /// <summary>
    /// Дополнительная Информация о пользователе
    /// </summary>
    public UserInfo UserInfo { get; set; } = default!;
    
    public Premium Subscriptions { get; set; } = default!;
    
    /// <summary>
    /// Каналы
    /// </summary>
    public ICollection<Channel> Channels { get; set; } = default!;

    /// <summary>
    /// Транзакции
    /// </summary>
    public ICollection<Transaction> Transactions { get; set; } = default!;
    
    /// <summary>
    /// nav-prop Файл
    /// </summary>
    public Guid AvatarId { get; set; }

    /// <summary>
    /// Аватаркa
    /// </summary>
    public File AvatarUrl { get; set; } = default!;
}
