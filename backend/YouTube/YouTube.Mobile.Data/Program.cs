using Microsoft.EntityFrameworkCore;
using YouTube.Application.Interfaces;
using YouTube.Application.Interfaces.Repositories;
using YouTube.Mobile.Data.Data.Mutations;
using YouTube.Mobile.Data.Data.Queries;
using YouTube.Payment.Data.Extensions;
using YouTube.Persistence.Contexts;
using YouTube.Persistence.Repositories;
using YouTube.Proto;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddGraphQLServer()
    .AddQueryType(m => m.Name("Query"))
    .AddMutationType(m => m.Name("Mutation"))
    .AddTypeExtension<ChannelQuery>() 
    .AddTypeExtension<UserQuery>()  
    .AddTypeExtension<PaymentQuery>()
    .AddTypeExtension<VideoQuery>()
    .AddTypeExtension<PaymentMutation>()
    .AddErrorFilter(error => error.WithMessage(error.Exception?.Message ?? "Unknown error"))
    .BindRuntimeType<Guid, IdType>(); 

//builder.Services.AddS3Storage(builder.Configuration);
var connectionString = builder.Configuration.GetConnectionString("Postgres");

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
    options.InstanceName = "Redis";
}); 

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Postgres")));

// Регистрация интерфейса IDbContext
builder.Services.AddScoped<IDbContext>(provider => 
    provider.GetRequiredService<ApplicationDbContext>());

// Регистрация репозиториев
builder.Services
    .AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>))
    .AddScoped<IUserRepository, UserRepository>()
    .AddScoped<IVideoRepository, VideoRepository>()
    .AddScoped<IChatRepository, ChatRepository>()
    .AddScoped<IChannelRepository, ChannelRepository>();


builder.Services.AddPaymentDbContext();

builder.Services.AddGrpcClient<PaymentService.PaymentServiceClient>(options =>
{
    options.Address = new Uri(builder.Configuration["PaymentService:GrpcEndpoint"]!);
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

app.UseCors("AllowAll");
app.MapGraphQL(); 

app.Run();

