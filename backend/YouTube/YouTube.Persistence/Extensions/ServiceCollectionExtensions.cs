using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using YouTube.Application.Interfaces;
using YouTube.Application.Interfaces.Repositories;
using YouTube.Domain.Entities;
using YouTube.Persistence.Contexts;
using YouTube.Persistence.MigrationTools;
using YouTube.Persistence.Repositories;
using YouTube.Persistence.Seeder;

namespace YouTube.Persistence.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddPersistenceLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext(configuration);
        services.AddContextAndRepositories();
        services.AddHttpContextAccessor();
    }
    
    private static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Postgres");

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString,
                builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
        
        services.AddIdentity<User, IdentityRole<Guid>>(opt =>
            {
                opt.Password.RequireDigit = false;
                opt.Password.RequiredLength = 6;
                opt.Password.RequireUppercase = true;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireLowercase = true;
                opt.SignIn.RequireConfirmedEmail = false;
                opt.SignIn.RequireConfirmedPhoneNumber = false;
                opt.User.RequireUniqueEmail = true;
                opt.User.RequireUniqueEmail = true;
                opt.SignIn.RequireConfirmedEmail = true;
            })
            .AddDefaultTokenProviders()
            .AddEntityFrameworkStores<ApplicationDbContext>();
    }


    private static void AddContextAndRepositories(this IServiceCollection services)
    {
        services
            .AddScoped<IDbContext, ApplicationDbContext>()
            .AddScoped<IDbSeeder, DbSeeder>()
            .AddTransient<Migrator>()
            .AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>))
            .AddScoped<IUserRepository, UserRepository>()
            .AddScoped<IVideoRepository, VideoRepository>();
    }
}