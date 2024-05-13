using System.Reflection;
using Microsoft.Extensions.FileProviders;
using YouTube.Application.Extensions;
using YouTube.Infrastructure.Extensions;
using YouTube.Infrastructure.SignalR;
using YouTube.Persistence.Extensions;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructureLayer();
builder.Services.AddPersistenceLayer(builder.Configuration);
builder.Services.AddApplicationLayer();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
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

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors(b => b
    .WithOrigins("http://localhost:5173", "http://localhost:5172") 
    .AllowAnyMethod()                     
    .AllowAnyHeader()                      
    .AllowCredentials());  

app.MapHub<EmailConfirmationHub>("/emailConfirmationHub");

app.UseFileServer(new FileServerOptions  
{  
    FileProvider = new PhysicalFileProvider(  
        Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")),  
    RequestPath = "/static",  
    EnableDefaultFiles = true  
}); 

app.MapControllers();
app.UseAuthentication();
app.UseAuthorization();

app.Run();