using YouTube.Domain.Entities;

namespace YouTube.Persistence.Configurations;

public class DatabaseSeeder
{
    public static IReadOnlyCollection<Avatar> Avatars() =>
        [
            new Avatar {Id = 1, Path = $"http://localhost:5041/static/avatars/users-1.svg"},
            new Avatar {Id = 2, Path = $"http://localhost:5041/static/avatars/users-2.svg"},
            new Avatar {Id = 3, Path = $"http://localhost:5041/static/avatars/users-3.svg"},
            new Avatar {Id = 4, Path = $"http://localhost:5041/static/avatars/users-4.svg"},
            new Avatar {Id = 5, Path = $"http://localhost:5041/static/avatars/users-5.svg"},
            new Avatar {Id = 6, Path = $"http://localhost:5041/static/avatars/users-6.svg"},
            new Avatar {Id = 7, Path = $"http://localhost:5041/static/avatars/users-7.svg"},
            new Avatar {Id = 8, Path = $"http://localhost:5041/static/avatars/users-8.svg"},
            new Avatar {Id = 9, Path = $"http://localhost:5041/static/avatars/users-9.svg"},
            new Avatar {Id = 10, Path = $"http://localhost:5041/static/avatars/users-10.svg"},
            new Avatar {Id = 11, Path = $"http://localhost:5041/static/avatars/users-11.svg"},
            new Avatar {Id = 12, Path = $"http://localhost:5041/static/avatars/users-12.svg"},
            new Avatar {Id = 13, Path = $"http://localhost:5041/static/avatars/users-13.svg"},
            new Avatar {Id = 14, Path = $"http://localhost:5041/static/avatars/users-14.svg"},
            new Avatar {Id = 15, Path = $"http://localhost:5041/static/avatars/users-15.svg"},
            new Avatar {Id = 16, Path = $"http://localhost:5041/static/avatars/users-16.svg"}
        ];
}