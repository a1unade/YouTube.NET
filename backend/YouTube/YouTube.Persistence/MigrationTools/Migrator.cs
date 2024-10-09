using Microsoft.EntityFrameworkCore;
using YouTube.Persistence.Contexts;
using YouTube.Persistence.Seeder;

namespace YouTube.Persistence.MigrationTools;

/// <summary>
/// Мигратор для наката миграций и сидов 
/// </summary>
public class Migrator
{
    private readonly ApplicationDbContext _context;
    private readonly IDbSeeder _seeder;

    public Migrator(ApplicationDbContext context, IDbSeeder seeder)
    {
        _context = context;
        _seeder = seeder;
    }
    
    /// <summary>
    /// Мигратор
    /// </summary>
    public async Task MigrateAsync()
    {
        try
        {
            await _context.Database.MigrateAsync().ConfigureAwait(false);
            await _seeder.SeedAsync(_context);
        }
        catch (Exception e)
        {
            Console.WriteLine($"migrations apply failed {e.Message}");
            throw;
        }
    }
}