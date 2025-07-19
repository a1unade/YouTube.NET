using System.Reflection;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Prometheus;
using YouTube.Application.Extensions;
using YouTube.Data.S3.Extensions;
using YouTube.Infrastructure.Extensions;
using YouTube.Infrastructure.Hubs;
using YouTube.Infrastructure.Services;
using YouTube.Persistence.Extensions;
using YouTube.Persistence.MigrationTools;
using YouTube.Shared.Serilog;
using YouTube.Shared.Telemetry;
using YouTube.WebAPI.Configurations;
using YouTube.WebAPI.Jobs;

var builder = WebApplication.CreateBuilder(args);

builder.Host.AddSerilog();

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(8080, listenOptions =>
    {
        listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
    });
    
    options.ListenAnyIP(8081, listenOptions =>
    {
        listenOptions.Protocols = HttpProtocols.Http2;
    });
});

builder.Services.AddGrpc();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddApplicationLayer(builder.Configuration);
builder.Services.AddInfrastructureLayer(builder.Configuration);

builder.Services.AddS3Storage(builder.Configuration);
builder.Services.AddPersistenceLayer(builder.Configuration);
builder.Services.AddRedis(builder.Configuration);

builder.Services.AddHostedService<RedisCleanupBackgroundService>();

builder.Services.AddPrometheus(builder.Configuration);
builder.Services.AddSwaggerGen(options =>
{
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", corsPolicyBuilder =>
    {
        corsPolicyBuilder
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});
    
var app = builder.Build();

using var scoped = app.Services.CreateScope();
var migrator = scoped.ServiceProvider.GetRequiredService<Migrator>();
await migrator.MigrateAsync();
//app.UseSerilogRequestLogging();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCustomExceptionMiddleware();

app.UseRouting();

app.UseCors("AllowAll");

app.MapHub<SupportChatHub>("/supportChatHub");
app.MapGrpcService<GrpcChatService>();

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpMetrics(); 
app.MapMetrics(); 

app.MapControllers();
app.Run();