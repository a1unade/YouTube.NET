using Microsoft.EntityFrameworkCore;
using YouTube.Payment.Data.Entities;
using YouTube.Payment.Data.Interfaces;

namespace YouTube.Payment.Data.Context;

public class PaymentContext : DbContext, IPaymentContext
{
    public PaymentContext(DbContextOptions<PaymentContext> options) : base(options)
    {
        
    }

    public DbSet<Wallet> Wallets { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(Extensions.ServiceCollectionExtensions).Assembly);
        base.OnModelCreating(modelBuilder);
        
    }
}