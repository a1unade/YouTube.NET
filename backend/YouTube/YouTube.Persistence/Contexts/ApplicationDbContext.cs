using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using YouTube.Application.Interfaces;
using YouTube.Domain.Entities;
using YouTube.Persistence.Configurations;

namespace YouTube.Persistence.Contexts;

public sealed class ApplicationDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>, IDbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<UserInfo> UserInfos { get; set; }
    public DbSet<Subscription> Subscriptions { get; set; }
    public DbSet<Avatar> Avatars { get; set; }
    public ApplicationDbContext(DbContextOptions<DbContext> options, IConfiguration configuration) 
        : base(options)
    {
    }
    
    public ApplicationDbContext()
    {
        Database.EnsureCreated();
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=6714;Username=postgres;Password=youtube;Database=postgres;");
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Avatar>().HasData(DatabaseSeeder.Avatars());
        
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new UserInfoConfiguration());
        modelBuilder.ApplyConfiguration(new SubscriptionConfiguration());
        
        
    }
}