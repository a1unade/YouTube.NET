
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using Users_account_management.Domain_Models;
using Users_account_management.Domain_Services;
using Users_account_management.Services;

namespace Users_account_management
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

         

            builder.Services.AddControllers();
           
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<UserDbContext>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddSingleton<IConnectionMultiplexer>(provider =>
            {
                ConfigurationOptions options = ConfigurationOptions.Parse("localhost:6379");
                return ConnectionMultiplexer.Connect(options);
            });
            builder.Services.AddScoped<IPasswordResetService, PasswordResetService>();
            builder.Services.AddAutoMapper(typeof(Program).Assembly);

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run("http://localhost:8080");
        }
    }
}
