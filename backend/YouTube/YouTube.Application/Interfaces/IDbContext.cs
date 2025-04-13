using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using YouTube.Domain.Entities;
using File = YouTube.Domain.Entities.File;

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
    public DbSet<Premium> Premiums{ get; set; }
    
    /// <summary>
    /// Подписки на каналы
    /// </summary>
    public DbSet<ChannelSubscription> ChannelSubscriptions { get; set; }
    
    /// <summary>
    /// Каналы
    /// </summary>
    public DbSet<Channel> Channels { get; set; }
    
    /// <summary>
    /// Комментарии
    /// </summary>
    public DbSet<Comment> Comments { get; set; }
    
    /// <summary>
    /// Видео
    /// </summary>
    public DbSet<Video> Videos { get; set; }
    
    /// <summary>
    /// Категории
    /// </summary>
    public DbSet<Category> Categories { get; set; }
    
    /// <summary>
    /// Файлы
    /// </summary>
    public DbSet<File> Files { get; set; }
    
    /// <summary>
    /// Плейлисты
    /// </summary>
    public DbSet<Playlist> Playlists { get; set; }
    
    /// <summary>
    /// Посты
    /// </summary>
    public DbSet<Post> Posts { get; set; }
    
    /// <summary>
    /// Транзакции
    /// </summary>
    public DbSet<Transaction> Transactions { get; set; }
    
    /// <summary>
    /// История чата
    /// </summary>
    public DbSet<ChatHistory> ChatHistories { get; set; }

    /// <summary>
    /// Сообщения чата
    /// </summary>
    public DbSet<ChatMessage> ChatMessages { get; set; }
    
    /// <summary>
    /// Роли
    /// </summary>
    public DbSet<Role> Roles { get; set; }
    
    /// <summary>
    /// Ссылки на соц сети
    /// </summary>
    public DbSet<Link> Links { get; set; }
    
    /// <summary>
    /// Начать транзакцию
    /// </summary>
    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Получить текущую транзакцию
    /// </summary>
    IDbContextTransaction? CurrentTransaction { get; }
    
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        
    DbSet<T> Set<T>() where T : class;
}