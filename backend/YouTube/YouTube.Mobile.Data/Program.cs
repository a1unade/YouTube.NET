using YouTube.Data.S3.Extensions;
using YouTube.Mobile.Data.Data.Mutations;
using YouTube.Mobile.Data.Data.Queries;
using YouTube.Payment.Data.Extensions;
using YouTube.Persistence.Extensions;
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

builder.Services.AddS3Storage(builder.Configuration);
builder.Services.AddPersistenceLayer(builder.Configuration);
builder.Services.AddPaymentDbContext();

builder.Services.AddGrpcClient<PaymentService.PaymentServiceClient>(options =>
{
    options.Address = new Uri(builder.Configuration["PaymentService:GrpcEndpoint"]!);
});
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
    options.InstanceName = "Redis";
}); 
var app = builder.Build();

app.MapGraphQL(); 

app.Run();

