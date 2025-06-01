using System.Reflection;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using YouTube.Application.Extensions;
using YouTube.Data.S3.Extensions;
using YouTube.Infrastructure.Extensions;
using YouTube.Infrastructure.Hubs;
using YouTube.Infrastructure.Services;
using YouTube.Persistence.Extensions;
using YouTube.Persistence.MigrationTools;
using YouTube.WebAPI.Configurations;
using YouTube.WebAPI.Jobs;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
    // HTTP/1.1 и HTTP/2 (например, для Swagger, REST API) на 8080
    options.ListenAnyIP(8080, listenOptions =>
    {
        listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
    });

    // Чистый HTTP/2 для gRPC на 8081
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

builder.Services.AddSwaggerGen(options =>
{
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});


builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAll", builder =>
        {
            builder
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
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
//app.UseHttpsRedirection();
app.UseRouting();

app.UseCors("AllowAll");

app.MapHub<SupportChatHub>("/supportChatHub");
app.MapGrpcService<GrpcChatService>();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();