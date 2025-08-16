using System.Reflection;
using AspNetCoreRateLimit;
using Hangfire;
using Prometheus;
using YouTube.Application.Extensions;
using YouTube.Data.S3.Extensions;
using YouTube.Infrastructure.Extensions;
using YouTube.Infrastructure.Hubs;
using YouTube.Infrastructure.Services;
using YouTube.Persistence.Extensions;
using YouTube.Persistence.MigrationTools;
using YouTube.Shared.Configurations.Auth;
using YouTube.Shared.Configurations.Cors;
using YouTube.Shared.Configurations.Hangfire;
using YouTube.Shared.Configurations.Kestrel;
using YouTube.Shared.Configurations.RateLimit;
using YouTube.Shared.Configurations.Redis;
using YouTube.Shared.Configurations.Serilog;
using YouTube.WebAPI.Configurations;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Host.AddSerilog();

builder.WebHost.ConfigureKestrel();

builder.Services.AddGrpc();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddApplicationLayer(builder.Configuration);
builder.Services.AddInfrastructureLayer(builder.Configuration);

builder.Services.AddS3Storage(builder.Configuration);
builder.Services.AddPersistenceLayer(builder.Configuration);
builder.Services.AddRedis(builder.Configuration);

builder.Services.AddAuth(builder.Configuration);

//builder.Services.AddPrometheus(builder.Configuration);

builder.Services.AddHangfire(builder.Configuration);
builder.Services.AddSwaggerGen(options =>
{
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Services.AddCustomCors();

builder.Services.AddRateLimit(builder.Configuration);

var app = builder.Build();

using var scoped = app.Services.CreateScope();
var migrator = scoped.ServiceProvider.GetRequiredService<Migrator>();
await migrator.MigrateAsync();

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

app.RegisterHangfireJobs(builder.Configuration);

//app.UseHttpMetrics(); 
//app.MapMetrics(); 

app.MapControllers();
app.UseHangfireDashboard();
app.UseIpRateLimiting();

app.Run();