using Microsoft.EntityFrameworkCore;
using YouTube.Domain.Entities;

namespace YouTube.Application.Interfaces;

public interface IDbContext
{
    /// <summary>
    /// Пользователи
    /// </summary>
    public DbSet<User> Users { get; set; }

    /// <summary>
    /// Информация о пользователе
    /// </summary>
    public DbSet<UserInfo> UserInfos { get; set; }
    
    /// <summary>
    /// Подписки
    /// </summary>
    public DbSet<Subscription> Subscriptions{ get; set; }
 
}