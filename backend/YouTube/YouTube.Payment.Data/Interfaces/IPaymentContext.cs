using Microsoft.EntityFrameworkCore;
using YouTube.Payment.Data.Entities;

namespace YouTube.Payment.Data.Interfaces;

public interface IPaymentContext
{
    public DbSet<Wallet> Wallets { get; set; }
    
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        
    DbSet<T> Set<T>() where T : class;
}