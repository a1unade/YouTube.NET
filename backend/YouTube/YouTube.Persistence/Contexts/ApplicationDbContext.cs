using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using YouTube.Application.Interfaces;
using YouTube.Domain.Entities;
using File = YouTube.Domain.Entities.File;

namespace YouTube.Persistence.Contexts;

public sealed class ApplicationDbContext : IdentityDbContext<User, Role, Guid>, IDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
        : base(options)
    {
    }
    
    public override DbSet<User> Users { get; set; }
    
    public DbSet<UserInfo> UserInfos { get; set; }
    
    public DbSet<Premium> Premiums { get; set; }
    
    public DbSet<ChannelSubscription> ChannelSubscriptions { get; set; }
    
    public DbSet<Channel> Channels { get; set; }
    
    public DbSet<Comment> Comments { get; set; }
    
    public DbSet<Video> Videos { get; set; }
    
    public DbSet<Category> Categories { get; set; }
    
    public DbSet<Transaction> Transactions { get; set; }

    public DbSet<Playlist> Playlists { get; set; }
    
    public DbSet<Post> Posts  { get; set; }
    
    public DbSet<ChatHistory> ChatHistories { get; set; }
    
    public DbSet<ChatMessage> ChatMessages { get; set; }

    public DbSet<File> Files { get; set; }
    
    public DbSet<Link> Links { get; set; }
    
    public IDbContextTransaction? CurrentTransaction
        => Database.CurrentTransaction;
    
    public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default) 
        => await Database.BeginTransactionAsync(cancellationToken);
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(Extensions.ServiceCollectionExtensions).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}