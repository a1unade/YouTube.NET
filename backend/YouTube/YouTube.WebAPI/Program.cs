using System.Reflection;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using YouTube.Application.Extensions;
using YouTube.Data.S3.Extensions;
using YouTube.Infrastructure.Extensions;
using YouTube.Infrastructure.Hubs;
using YouTube.Persistence.Extensions;
using YouTube.Persistence.MigrationTools;
using YouTube.Proto;
using YouTube.WebAPI.Configurations;
using YouTube.WebAPI.Jobs;

var builder = WebApplication.CreateBuilder(args);
Console.WriteLine(builder.Configuration["PaymentService:GrpcEndpoint"]!);

builder.WebHost.ConfigureKestrel(options =>
{
    options.ConfigureEndpointDefaults(lo => 
    {
        lo.Protocols = HttpProtocols.Http1AndHttp2;
    });
});



builder.Services.AddGrpcClient<PaymentService.PaymentServiceClient>(options =>
{
    options.Address = new Uri("http://localhost:8085");
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddApplicationLayer();
builder.Services.AddInfrastructureLayer(builder.Configuration);

builder.Services.AddS3Storage(builder.Configuration);
builder.Services.AddPersistenceLayer(builder.Configuration);
builder.Services.AddRedis(builder.Configuration);

builder.Services.AddHostedService<RedisCleanupBackgroundService>();

builder.Services.AddSwaggerGen(options =>
{
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});


var app = builder.Build();

using var scoped = app.Services.CreateScope();
var migrator = scoped.ServiceProvider.GetRequiredService<Migrator>();
await migrator.MigrateAsync();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// TODO  (Logging)
app.UseCustomExceptionMiddleware();
app.UseHttpsRedirection();
app.UseRouting();

app.UseCors(b => b
    .WithOrigins("http://localhost:5173", "http://localhost:5172", "http://localhost:5174", "http://localhost:3000") 
    .AllowAnyMethod()                     
    .AllowAnyHeader()                      
    .AllowCredentials());  

app.MapHub<SupportChatHub>("/supportChatHub");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();