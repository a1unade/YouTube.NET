using YouTube.BusAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddMessageBus(builder.Configuration);

var app = builder.Build();

app.UseHttpsRedirection();
app.UseRouting();

app.Run();