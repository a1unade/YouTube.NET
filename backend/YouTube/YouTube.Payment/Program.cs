using System.Net;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using YouTube.Payment.Data.Extensions;
using YouTube.Payment.Data.Tools;
using YouTube.Payment.Services;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
    options.Listen(IPAddress.Any, 8085, listenOptions =>
    {
        listenOptions.Protocols = HttpProtocols.Http2;
    });
});

builder.Services.AddGrpc();
builder.Services.AddPaymentDbContext();
builder.Services.AddLogging(configure => configure.AddConsole());

var app = builder.Build();

try
{
    using var scope = app.Services.CreateScope();
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
    logger.LogInformation("Starting database migration...");
    
    var migrator = scope.ServiceProvider.GetRequiredService<Migrator>();
    await migrator.MigrateAsync();
    
    logger.LogInformation("Database migration completed successfully");
}
catch (Exception ex)
{
    var logger = app.Services.GetRequiredService<ILogger<Program>>();
    logger.LogCritical(ex, "Database migration failed");
    throw;
}

app.UseRouting();
app.MapGrpcService<PaymentGrpcService>();

app.Run();