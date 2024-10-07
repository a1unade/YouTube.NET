using Microsoft.EntityFrameworkCore;
using YouTube.Application.Common.Enums;
using YouTube.Application.Interfaces;
using YouTube.Domain.Entities;

namespace YouTube.Persistence.Seeder;

public class DbSeeder : IDbSeeder
{
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
    
    private static async Task SeedCategoriesAsync(IDbContext context, CancellationToken cancellationToken)
    {
        var existingCategories = await context.Categories.ToListAsync(cancellationToken);

        var newCategories = _baseCategories
            .Where(baseCategory => existingCategories.All(existingCategory => existingCategory.Name != baseCategory.ToString()))
            .ToList();

        if (newCategories.Any())
        {
            context.Categories.AddRange(newCategories.Select(category => new Category { Name = category.ToString() }));
            await context.SaveChangesAsync(cancellationToken);
        }
    }

    
    public async Task SeedAsync(IDbContext context, CancellationToken cancellationToken = default)
    {
        await SeedCategoriesAsync(context, cancellationToken);
    }
}