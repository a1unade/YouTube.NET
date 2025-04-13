using Microsoft.EntityFrameworkCore;
using YouTube.Payment.Data.Context;

namespace YouTube.Payment.Data.Tools;

public class Migrator
{
    private readonly PaymentContext _context;

    public Migrator(PaymentContext context)
    {
        _context = context;
    }
    
    public async Task MigrateAsync()
    {
        try
        {
            await _context.Database.MigrateAsync().ConfigureAwait(false);
        }
        catch (Exception e)
        {
            Console.WriteLine($"migrations apply failed {e.Message}");
            throw;
        }
    }
}