using Microsoft.EntityFrameworkCore;
using YouTube.Persistence.Contexts;

namespace YouTube.UnitTests.Builders;

public abstract class ContextFactory
{

    private const string UserId = "53afbb05-bb2d-45e0-8bef-489ef1cd6fdc";
    
    public static ApplicationDbContext Create()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var context = new ApplicationDbContext(options);

        context.Database.EnsureCreated();

        var user = UserBuilder.CreateBuilder()
            .SetId(UserId)
            .SetUsername("Ilya")
            .SetBirthday(new DateOnly(2004, 01, 09))
            .SetEmail("bulatfri18@gmail.com")
            .SetUserInfo()
            .Build();

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