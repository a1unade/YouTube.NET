using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using YouTube.Proto;

namespace YouTube.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddApplicationLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediator();
        services.AddValidators();
        services.AddHttpContextAccessor();
        services.AddGrpcClient(configuration);
    }
    
    private static void AddMediator(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
    }

    private static void AddValidators(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
    }     
    
    private static void AddGrpcClient(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddGrpcClient<PaymentService.PaymentServiceClient>(options =>
        {
            options.Address = new Uri(configuration["PaymentService:GrpcEndpoint"]!);
        });
    }    
}