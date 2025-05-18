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
        
        //var connectionString = "Host=localhost;Username=postgres;Password=Bulat2004;Database=payment_db";
        var connectionString = "Host=payment_db;Username=postgres;Password=youtube;Database=payment_db";
        //var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__PaymentDb");

        optionBuilder.UseNpgsql(connectionString);
        
        return new PaymentContext(optionBuilder.Options);
    }
}