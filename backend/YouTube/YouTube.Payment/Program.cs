using System.Net;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using YouTube.Payment.Data.Context;
using YouTube.Payment.Data.Interfaces;
using YouTube.Payment.Data.Options;
using YouTube.Payment.Data.Repositories;
using YouTube.Payment.Services;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
    options.Listen(IPAddress.Any, 8085, listenOptions =>
    {
        listenOptions.Protocols = HttpProtocols.Http2;
    });
    options.ConfigureEndpointDefaults(lo => 
    {
        lo.Protocols = HttpProtocols.Http2;
    });
    
});

builder.Services.AddGrpc(); 

builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDbSettings"));
builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
    return new MongoClient("mongodb://localhost:27017");
});
builder.Services.AddScoped<IMongoContext>(s =>
{
    var context = new MongoContext(s.GetRequiredService<IMongoClient>());
    return context;
});
        
builder.Services.AddScoped<IWalletRepository, WalletRepository>();

var app = builder.Build();

app.UseRouting();

app.MapGrpcService<PaymentGrpcService>();
app.Run();

