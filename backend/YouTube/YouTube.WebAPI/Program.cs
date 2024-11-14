using System.Reflection;
using YouTube.Application.Extensions;
using YouTube.Infrastructure.Extensions;
using YouTube.Infrastructure.Hubs;
using YouTube.MessageBus.Extensions;
using YouTube.MessageBus.Options;
using YouTube.Persistence.Extensions;
using YouTube.Persistence.MigrationTools;
using YouTube.WebAPI.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddApplicationLayer();

//builder.Services.Configure<RabbitOptions>(IConfiguration.GetSection("RabbitOptions"));
builder.Services.AddMessageBus(builder.Configuration.GetSection(nameof(RabbitOptions)).Get<RabbitOptions>()!);
builder.Services.AddInfrastructureLayer(builder.Configuration);

builder.Services.AddPersistenceLayer(builder.Configuration);
builder.Services.AddRedis(builder.Configuration);

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