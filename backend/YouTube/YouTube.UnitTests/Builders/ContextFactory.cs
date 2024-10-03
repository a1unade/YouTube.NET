using Microsoft.EntityFrameworkCore;
using YouTube.Domain.Entities;
using YouTube.Persistence.Contexts;

namespace YouTube.UnitTests.Builders;

public abstract class ContextFactory
{
    public static ApplicationDbContext Create(User user)
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var context = new ApplicationDbContext(options);

        context.Database.EnsureCreated();
        
        context.Users.AddRange(user);

        context.SaveChangesAsync();
        
        return context;
    }

    public static void Destroy(ApplicationDbContext context)
    {
        context.Database.EnsureDeleted();
        context.Dispose();
    }
}