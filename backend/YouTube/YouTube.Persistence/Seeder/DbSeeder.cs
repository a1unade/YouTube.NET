using ClickHouse.Client.ADO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using YouTube.Application.Common.Enums;
using YouTube.Application.Common.Exceptions;
using YouTube.Application.DTOs.File;
using YouTube.Application.Interfaces;
using YouTube.Domain.ClickHouseEntity;
using YouTube.Domain.Entities;
using File = YouTube.Domain.Entities.File;

namespace YouTube.Persistence.Seeder;

public class DbSeeder : IDbSeeder, IDisposable
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;
    private readonly IS3Service _s3Service;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly ClickHouseConnection _clickHouseConnection;
    private const string ClickHouseTableName = nameof(View);

    public DbSeeder(
        UserManager<User> userManager,
        RoleManager<Role> roleManager,
        IS3Service s3Service,
        IWebHostEnvironment webHostEnvironment,
        IConfiguration configuration)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _s3Service = s3Service;
        _webHostEnvironment = webHostEnvironment;
        _clickHouseConnection = new ClickHouseConnection(configuration["ConnectionStrings:ClickHouse"]);
        _clickHouseConnection.Open();
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
        DisplayName = "Main Admin",
        UserInfo = new UserInfo
        {
            Name = "ADMIN",
            Surname = "ADMIN",
            BirthDate = default,
            Gender = "male"
        },
        EmailConfirmed = true
    };

    public async Task SeedAsync(IDbContext context, CancellationToken cancellationToken = default)
    {
        await SeedRolesAsync(_roleManager, context, cancellationToken);
        await SeedAdminAsync(context, cancellationToken);
        await SeedCategoriesAsync(context, cancellationToken);
        await SeedBaseChannelsAsync(context, cancellationToken);
        await SeedUserAsync(context, cancellationToken);
        await SeedUserChannelAndLinksAsync(context, cancellationToken);
        await SeedVideoAsync(context, cancellationToken);
        await SeedPlaylistAsync(context, cancellationToken);

        await context.SaveChangesAsync(cancellationToken);

        await CreateClickHouseTable(cancellationToken);
        await SeedVideoOnClickHouse(context, cancellationToken);
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
                    User = _user
                }), cancellationToken);
    }

    private static async Task SeedRolesAsync(RoleManager<Role> roleManager, IDbContext context,
        CancellationToken cancellationToken)
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

    private async Task SeedUserAsync(IDbContext context, CancellationToken cancellationToken)
    {
        var existingUser = await _userManager.FindByEmailAsync("ashab@gmail.com");

        if (existingUser == null)
        {
            var user = new User
            {
                UserName = "ashab@gmail.com",
                Email = "ashab@gmail.com",
                DisplayName = "Асхаб Тамаев",
                UserInfo = new UserInfo
                {
                    Name = "Асхаб",
                    Surname = "Тамаев",
                    BirthDate = default,
                    Gender = "Male",
                    Country = "Russia"
                }
            };

            var result = await _userManager.CreateAsync(user, "ashab123!");
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");
                await context.SaveChangesAsync(cancellationToken);
            }
        }
    }

    private async Task SeedUserChannelAndLinksAsync(IDbContext context, CancellationToken cancellationToken)
    {
        var user = await context.Users.FirstOrDefaultAsync(x => x.Email == "ashab@gmail.com", cancellationToken);
        var channel = await context.Channels
            .Include(x => x.Links)
            .FirstOrDefaultAsync(x => x.Name == "Tamaev TV", cancellationToken);

        if (channel == null && user != null)
        {
            channel = new Channel
            {
                Name = "Tamaev TV",
                Description = "Это моя машина",
                CreateDate = DateOnly.FromDateTime(DateTime.Now),
                SubCount = 231232,
                Country = "Russia",
                User = user
            };

            await context.Channels.AddAsync(channel, cancellationToken);
        }

        var channelImage = await context.Files
            .FirstOrDefaultAsync(x => x.Path == channel!.Id + "ashab.jpg", cancellationToken);

        if (channelImage == null)
        {
            var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images/ashab.jpg");

            if (!System.IO.File.Exists(filePath))
                throw new NotFoundException("Файл изображения не найден");

            await using var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

            var path = await _s3Service.UploadAsync(new FileContent
            {
                Content = fileStream,
                FileName = "ashab.jpg",
                ContentType = "image/jpeg",
                Length = fileStream.Length,
                Bucket = channel!.Id.ToString()
            }, cancellationToken);

            var file = new File
            {
                Size = fileStream.Length,
                ContentType = "image/jpeg",
                Path = path,
                FileName = "ashab.jpg",
                BucketName = channel.Id.ToString()
            };

            await context.Files.AddAsync(file, cancellationToken);
            channel.MainImgFile = file;
        }

        if (channel!.Links == null! || !channel.Links.Any())
        {
            channel.Links = new List<Link>
            {
                new() { Reference = "https://vk.com/id446657723", Channel = channel },
                new() { Reference = "https://t.me/BuLbl4_13", Channel = channel },
                new() { Reference = "https://steamcommunity.com/profiles/76561199096782472", Channel = channel }
            };
            await context.Links.AddRangeAsync(channel.Links, cancellationToken);
        }

        await context.SaveChangesAsync(cancellationToken);
    }
    
    private async Task SeedVideoAsync(IDbContext context, CancellationToken cancellationToken)
    {
        var video = await context.Videos
            .FirstOrDefaultAsync(x => x.Name == "Mohito" && x.Description == "Tamaev Mohito", cancellationToken);

        var channel = await context.Channels.FirstOrDefaultAsync(x => x.Name == "Tamaev TV", cancellationToken);

        if (channel != null)
        {
            if (video is null)
            {
                video = new Video
                {
                    Name = "Mohito",
                    Description = "Tamaev Mohito",
                    ViewCount = 1245,
                    LikeCount = 663,
                    DisLikeCount = 234,
                    ReleaseDate = DateOnly.FromDateTime(DateTime.Now),
                    Country = "Russia",
                    Channel = channel
                };
                var videoPreview =
                    await context.Files.FirstOrDefaultAsync(x => x.Path == channel.Id + "preview.jpg",
                        cancellationToken);

                if (videoPreview is null)
                {
                    var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images/preview.jpg");

                    if (!System.IO.File.Exists(filePath))
                        throw new NotFoundException();

                    await using var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

                    var path = await _s3Service.UploadAsync(new FileContent
                    {
                        Content = fileStream,
                        FileName = "preview.jpg",
                        ContentType = "image/jpeg",
                        Length = fileStream.Length,
                        Bucket = channel.Id.ToString()
                    }, cancellationToken);

                    var file = new File
                    {
                        Size = fileStream.Length,
                        ContentType = "image/jpeg",
                        Path = path,
                        FileName = "preview.jpg",
                        BucketName = channel.Id.ToString()
                    };

                    await context.Files.AddAsync(file, cancellationToken);
                    video.PreviewImg = file;
                }

                var videoFile =
                    await context.Files.FirstOrDefaultAsync(x => x.Path == channel.Id + "mohito.mp4",
                        cancellationToken);

                if (videoFile is null)
                {
                    var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "video/mohito.mp4");

                    if (!System.IO.File.Exists(filePath))
                        throw new NotFoundException();

                    await using var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

                    var path = await _s3Service.UploadAsync(new FileContent
                    {
                        Content = fileStream,
                        FileName = "mohito.mp4",
                        ContentType = "video/mp4",
                        Length = fileStream.Length,
                        Bucket = channel.Id.ToString()
                    }, cancellationToken);

                    var file = new File
                    {
                        Size = fileStream.Length,
                        ContentType = "video/mp4",
                        Path = path,
                        FileName = "mohito.mp4",
                        BucketName = channel.Id.ToString()
                    };

                    await context.Files.AddAsync(file, cancellationToken);
                    video.VideoUrl = file;
                }

                await context.Videos.AddAsync(video, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);
            }
        }
    }
    
    private async Task SeedPlaylistAsync(IDbContext context, CancellationToken cancellationToken)
    {
        var playlists = await context.Playlists
            .AsNoTracking()
            .ToListAsync(cancellationToken);
        
        var channel = await context.Channels
            .FirstOrDefaultAsync(x => x.Name == "Tamaev TV", cancellationToken);
        
        var video = await context.Videos
            .FirstOrDefaultAsync(x => x.Name == "Mohito" && x.Description == "Tamaev Mohito", cancellationToken);

        if (!playlists.Any() && channel != null && video != null)
        {
            var playlist = new Playlist
            {
                Name = "Tamaev mems",
                Description = "Тамаев раздает стиля",
                CreateDate = DateOnly.FromDateTime(DateTime.Now),
                Videos = new List<Video> { video },
                ChannelId = channel.Id,
                Channel = channel
            };

            await context.Playlists.AddAsync(playlist, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
        }
    }

    private async Task CreateClickHouseTable(CancellationToken cancellationToken)
    {
        var command =  $"EXISTS TABLE {ClickHouseTableName}";
        var cmdCheck = _clickHouseConnection.CreateCommand();
        cmdCheck.CommandText = command;
        var result = await cmdCheck.ExecuteScalarAsync(cancellationToken);
        var tableExist = Convert.ToBoolean(result);

        if (tableExist)
        {
            Console.WriteLine($"Таблица {ClickHouseTableName} уже существует");
            return;
        }
        
        var typeMap = new Dictionary<Type, string>
        {
            { typeof(Guid), "UUID" },
            { typeof(string), "String" },
            { typeof(int), "Int32" },
            { typeof(long), "Int64" },
            { typeof(ulong), "UInt64" },
            { typeof(DateTime), "DateTime" },
        };
        
        var properties = typeof(View).GetProperties();
        var columnDefinitions = new List<string>();

        foreach (var prop in properties)
        {
            var propType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
        
            if (!typeMap.TryGetValue(propType, out var clickHouseType))
            {
                throw new NotSupportedException($"Type '{propType.Name}' is not supported for property '{prop.Name}'");
            }

            columnDefinitions.Add($"{prop.Name} {clickHouseType}");
        }

        var sql = $@"
        CREATE TABLE IF NOT EXISTS {ClickHouseTableName}
        (
            {string.Join(",\n            ", columnDefinitions)}
        )
        ENGINE = MergeTree()
        ORDER BY (Id)";  

        await using var cmd = _clickHouseConnection.CreateCommand();
        cmd.CommandText = sql;
        await cmd.ExecuteNonQueryAsync(cancellationToken);
    }

    private async Task SeedVideoOnClickHouse(IDbContext context, CancellationToken cancellationToken)
    {
        var video = await context.Videos
            .FirstOrDefaultAsync(x => x.Name == "Mohito" && x.Description == "Tamaev Mohito", cancellationToken);

        if (video == null)
        {
            return;
        }

        var checkQuery = $@"SELECT COUNT() FROM {ClickHouseTableName} WHERE VideoId = '{video.Id}'";
        
        await using var checkCmd = _clickHouseConnection.CreateCommand();
        checkCmd.CommandText = checkQuery;
        var countResult = Convert.ToInt64(await checkCmd.ExecuteScalarAsync(cancellationToken));
        
        if (countResult > 0)
        {
            Console.WriteLine($"Видео с ID '{video.Id}' уже существует в ClickHouse");
            return;
        }
        
        var query = $@"
        INSERT INTO {ClickHouseTableName} 
        (Id, VideoName, VideoId, ChannelId, ViewCount)
        VALUES 
        (
            generateUUIDv4(), 
            '{video.Name}', 
            '{video.Id}', 
            '{video.ChannelId}', 
            {0}
        )";

        await using var cmd = _clickHouseConnection.CreateCommand();
        cmd.CommandText = query;
        await cmd.ExecuteNonQueryAsync(cancellationToken);
    }

    public void Dispose()
    {
        try
        {
            _userManager.Dispose();
            _roleManager.Dispose();
            (_clickHouseConnection as IDisposable).Dispose();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при освобождении ресурсов: {ex.Message}");
        }
    }
}