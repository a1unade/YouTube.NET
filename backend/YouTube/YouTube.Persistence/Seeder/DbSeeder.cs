using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using YouTube.Application.Common.Enums;
using YouTube.Application.Interfaces;
using YouTube.Domain.Entities;

namespace YouTube.Persistence.Seeder;

public class DbSeeder : IDbSeeder
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;

    public DbSeeder(UserManager<User> userManager, RoleManager<Role> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    private static List<CategoryType> _baseCategories = new()
    {
        CategoryType.Humor,
        CategoryType.Science,
        CategoryType.Sport,
        CategoryType.Entertainment,
        CategoryType.Games,
        CategoryType.Music,
        CategoryType.MoviesAndAnimations
    };

    private static List<Role> _roles = new()
    {
        new Role("Admin"),
        new Role("User")
    };

    private static List<Channel> _baseChannels = new()
    {
        new Channel
        {
            Name = "Музыка",
            Description =
                "На этом канале собраны главные музыкальные новинки, а также видео и плейлисты популярных исполнителей." +
                " Подпишитесь и следите за музыкальными трендами во всем мире. Этот канал создан автоматически системой обнаружения видео YouTube.",
            CreateDate = DateOnly.FromDateTime(DateTime.Today),
        },
        new Channel
        {
            Name = "Фильмы",
            Description =
                "На этом канале собраны главные новинки мира кино, а также сериалы и мультики. Подпишитесь и следите за трендами во всем мире. Этот канал создан автоматически системой обнаружения видео YouTube.",
            CreateDate = DateOnly.FromDateTime(DateTime.Today)
        },
        new Channel
        {
            Name = "Спорт",
            CreateDate = DateOnly.FromDateTime(DateTime.Today)
        },
        new Channel
        {
            Name = "Видеоигры",
            CreateDate = DateOnly.FromDateTime(DateTime.Today)
        }
    };

    private static User _user = new()
    {
        UserName = "Admin",
        Email = "bulatfree18@gmail.com",
        UserInfo =  new UserInfo
        {
            Name = "ADMIN",
            Surname = "ADMIN",
            BirthDate = default,
            Gender = "male"
        }
    };

    public async Task SeedAsync(IDbContext context, CancellationToken cancellationToken = default)
    {
        await SeedRolesAsync(_roleManager, context, cancellationToken);
        await SeedAdminAsync(context, cancellationToken);
        await SeedCategoriesAsync(context, cancellationToken);
        await SeedBaseChannelsAsync(context, cancellationToken);

        await context.SaveChangesAsync(cancellationToken);
    }


    private static async Task SeedCategoriesAsync(IDbContext context, CancellationToken cancellationToken)
    {
        var existingCategories = await context.Categories.AsNoTracking().ToListAsync(cancellationToken);

        var newCategories = _baseCategories
            .Where(baseCategory =>
                existingCategories.All(existingCategory => existingCategory.Name != baseCategory.ToString()))
            .ToList();

        if (newCategories.Any())
            await context.Categories.AddRangeAsync(
                newCategories.Select(category => new Category { Name = category.ToString() }), cancellationToken);
    }
    
    private static async Task SeedBaseChannelsAsync(IDbContext context, CancellationToken cancellationToken)
    {
        var existingChannels = await context.Channels.AsNoTracking().ToListAsync(cancellationToken);

        var newChannels = _baseChannels
            .Where(baseChannel =>
                existingChannels.All(existingChannel => existingChannel.Name != baseChannel.Name))
            .ToList();
        
        if (newChannels.Any())
            await context.Channels.AddRangeAsync(
                newChannels.Select(channel => new Channel
                {
                    Name = channel.Name,
                    Description = channel.Description,
                    CreateDate = channel.CreateDate,
                    User = _user,
                }), cancellationToken);
    }
    
    private static async Task SeedRolesAsync(RoleManager<Role> roleManager, IDbContext context, CancellationToken cancellationToken)
    {
        foreach (var roleName in _roles)
        {
            if (!await roleManager.RoleExistsAsync(roleName.Name!))
            {
                await roleManager.CreateAsync(new Role { Name = roleName.Name });
            }
        }

        await context.SaveChangesAsync(cancellationToken);
    }

    private async Task SeedAdminAsync(IDbContext context, CancellationToken cancellationToken)
    {
        var existingUser = await _userManager.FindByEmailAsync(_user.Email!);
        if (existingUser == null)
        {
            var result = await _userManager.CreateAsync(_user, "Password123!");

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(_user, "Admin");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    Console.WriteLine(error.Description);
                }
            }
        }
        else
        {
            var isInRole = await _userManager.IsInRoleAsync(existingUser, "Admin");
            if (!isInRole)
            {
                await _userManager.AddToRoleAsync(existingUser, "Admin");
            }
        }

        await context.SaveChangesAsync(cancellationToken);
    }
}