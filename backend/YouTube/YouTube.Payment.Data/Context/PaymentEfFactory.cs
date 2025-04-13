using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace YouTube.Payment.Data.Context;

public class PaymentEfFactory : IDesignTimeDbContextFactory<PaymentContext>
{
    
    public PaymentContext CreateDbContext(string[] args)
    {
        var config = new ConfigurationBuilder().Build();
        
        var optionBuilder = new DbContextOptionsBuilder<PaymentContext>();
        
        optionBuilder.UseNpgsql("Host=payment_db;Username=postgres;Password=youtube;Database=payment_db");
        
        return new PaymentContext(optionBuilder.Options);
    }
}