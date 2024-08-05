using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using YouTube.Domain.Entities;
using File = YouTube.Domain.Entities.File;

namespace YouTube.Persistence.Contexts;

public sealed class ApplicationDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
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
    
    public DbSet<File> Files { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(Extensions.ServiceCollectionExtensions).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}