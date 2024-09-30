using System.Reflection;
using Microsoft.Extensions.FileProviders;
using YouTube.Application.Extensions;
using YouTube.Data.S3;
using YouTube.Infrastructure.Extensions;
using YouTube.Infrastructure.SignalR;
using YouTube.Persistence.Extensions;
using YouTube.WebAPI.Configurations;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddApplicationLayer();
builder.Services.AddInfrastructureLayer();

builder.Services.AddPersistenceLayer(builder.Configuration);
builder.Services.AddS3Storage(builder.Configuration);
builder.Services.AddRedis(builder.Configuration);

builder.Services.AddSwaggerGen(options =>
{
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

var app = builder.Build();

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
    .WithOrigins("http://localhost:5173", "http://localhost:5172") 
    .AllowAnyMethod()                     
    .AllowAnyHeader()                      
    .AllowCredentials());  

app.MapHub<EmailConfirmationHub>("/emailConfirmationHub");



app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();