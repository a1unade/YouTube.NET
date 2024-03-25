using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
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
            
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "your_issuer",
                    ValidAudience = "your_audience",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("VDcyNjM3ODYyMDY3MzQ1OTI2MzU1MDczNTY1Njc0NTI2NzQ1NzA2MzQ2MjY0NjA2MjM1Mzc3MDU0NjA1NTU2MzQ2NTc4NjE2MjU4NzMxNDY3Nzc3NjE0NjI2NDYwNjUzNDU1Njc4Njg3NDU0NzY1MzQxNDMzNDUyNjY2NTQzNDMwNTU2MzY0NzQ3NjU0NjA3MzU2MjYzNjU3NjM0NTUzNzQ2NzUxNjM0Mzg2NTI2NjQyNjE3NDU2NzE1MzY0MjY5MzY0MzU1Mzc0MzYzNDU0NjA0NzYzNjQxNjE2MzUzNzI"))
                };
            });
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
