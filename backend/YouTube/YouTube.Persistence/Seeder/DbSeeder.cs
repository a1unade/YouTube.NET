using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using YouTube.Application.Common.Enums;
using YouTube.Application.Interfaces;
using YouTube.Domain.Entities;

namespace YouTube.Persistence.Seeder;

public class DbSeeder : IDbSeeder
{
    private readonly UserManager<User> _userManager;

    public DbSeeder(UserManager<User> userManager)
    {
        _userManager = userManager;
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
        Email = "bulatfree18@gmail.com" ,
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
        await SeedAdminUserAsync(context, cancellationToken);
        await SeedCategoriesAsync(context, cancellationToken);
        await SeedBaseChannelsAsync(context, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    private static async Task SeedCategoriesAsync(IDbContext context, CancellationToken cancellationToken)
    {
        var existingCategories = await context.Categories.ToListAsync(cancellationToken);

        var newCategories = _baseCategories
            .Where(baseCategory =>
                existingCategories.All(existingCategory => existingCategory.Name != baseCategory.ToString()))
            .ToList();

        if (newCategories.Any())
            await context.Categories.AddRangeAsync(
                newCategories.Select(category => new Category { Name = category.ToString() }), cancellationToken);
    }

    private async Task SeedAdminUserAsync(IDbContext context, CancellationToken cancellationToken)
    {
        var existingUser = await context.Users.FirstOrDefaultAsync(x => x.Email == _user.Email || x.UserName == _user.UserName, cancellationToken);

        if (existingUser == null)
        {
            var result = await _userManager.CreateAsync(_user, "StrongAdminPassword123!");
            
            if (!result.Succeeded)
            {
                throw new Exception(string.Join("; ", result.Errors.Select(e => e.Description)));
            }
        }
    }


    private static async Task SeedBaseChannelsAsync(IDbContext context, CancellationToken cancellationToken)
    {
        var existingChannels = await context.Channels.ToListAsync(cancellationToken);

        var newChannels = _baseChannels
            .Where(baseChannels =>
                existingChannels.All(existingChannel => existingChannel.Name != baseChannels.ToString()))
            .ToList();
        if (newChannels.Any())
            await context.Channels.AddRangeAsync(
                newChannels.Select(channel => new Channel
                {
                    Name = channel.Name,
                    Description = channel.Description,
                    CreateDate = channel.CreateDate,
                    User = _user
                }), cancellationToken);
    }
}