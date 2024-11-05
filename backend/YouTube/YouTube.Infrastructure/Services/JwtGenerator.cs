using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using YouTube.Application.Interfaces;
using YouTube.Domain.Entities;
using YouTube.Infrastructure.Options;

namespace YouTube.Infrastructure.Services;

public class JwtGenerator : IJwtGenerator
{
    private readonly AuthOptions _options;
    private readonly UserManager<User> _userManager;
    public JwtGenerator(IConfiguration configuration, UserManager<User> userManager)
    {
        _userManager = userManager;
        _options = configuration.GetSection("JwtSettings").Get<AuthOptions>()!;
    }
    public async Task<string> GenerateToken(User user)
    {
        var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");
        
        Claim[] claims = 
        {
            new ("Id", user.Id.ToString()),
            new ("Email", user.Email!),
            new ("Name", user.DisplayName),
            new ("Role", isAdmin ? "Admin" : "User")
        };

        var issuer = _options.Issuer;
        var audience = _options.Audience;
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer,
            audience,
            claims,
            expires: DateTime.UtcNow.AddHours(3),
            signingCredentials: credentials
        );

        var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

        return tokenValue;
    }
}