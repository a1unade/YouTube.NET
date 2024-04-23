using Microsoft.AspNetCore.Identity;

namespace YouTube.Domain.Entities;

public class User : IdentityUser<Guid>
{
    /// <summary>
    /// Информация о пользователе пользователя
    /// </summary>
    public UserInfo UserInfo { get; set; }
    public ICollection<Subscription> Subscriptions { get; set; } = new HashSet<Subscription>();
}