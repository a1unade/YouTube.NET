using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using YouTube.Application.Common.Exceptions;
using YouTube.Application.DTOs.Auth;
using YouTube.Application.Interfaces;
using YouTube.Domain.Entities;
using YouTube.Infrastructure.Options;

namespace YouTube.Infrastructure.Services;

public class JwtService(
    IConfiguration configuration,
    UserManager<User> userManager,
    IDbContext context
) : IJwtService
{
    private readonly AuthOptions _options = configuration.GetSection("JwtSettings").Get<AuthOptions>()!;
    
    public async Task<string> GenerateAccessToken(User user)
    {
        var isAdmin = await userManager.IsInRoleAsync(user, "Admin");
        
        Claim[] claims =
        [
            new (JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new (JwtRegisteredClaimNames.Email, user.Email!),
            new (JwtRegisteredClaimNames.Name, user.DisplayName),
            new (ClaimTypes.Role, isAdmin ?  "Admin" : "User")
        ];
        
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _options.Issuer,
            audience : _options.Audience,
            claims : claims,
            expires: DateTime.UtcNow.AddDays(_options.AccessTokenActivityDays),
            signingCredentials: credentials
        );
        
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<RefreshToken> GenerateRefreshToken(User user, CancellationToken cancellationToken)
    {
        await RevokeAllRefreshTokens(user.Id, cancellationToken);
        
        var refreshToken = new RefreshToken
        {
            Token = Guid.NewGuid().ToString("N"),
            Expires = DateTime.UtcNow.AddDays(_options.RefreshTokenActivityDays),
            Created = DateTime.UtcNow,
            UserId = user.Id
        };
        
        await context.RefreshTokens.AddAsync(refreshToken, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return refreshToken;
    }
    
    public async Task<TokenModel> RefreshTokenPair(string refreshTokenValue, CancellationToken cancellationToken)
    {
        var refreshToken = await context.RefreshTokens
            .FirstOrDefaultAsync(rt => rt.Token == refreshTokenValue, cancellationToken);

        if (refreshToken == null || !refreshToken.IsActive)
            throw new SecurityTokenException("Invalid refresh token");
        
        var user = await context.Users.FirstOrDefaultAsync(u => u.Id == refreshToken.UserId, cancellationToken)
                   ?? throw new NotFoundException(typeof(User));
      
          
        var newAccessToken = await GenerateAccessToken(user);
        var newRefreshToken = await GenerateRefreshToken(user, cancellationToken);
        
        refreshToken.Revoked = DateTime.UtcNow;
        refreshToken.ReplacedByToken = newRefreshToken.Token;
        context.RefreshTokens.Update(refreshToken);
        
        await context.SaveChangesAsync(cancellationToken);

        return new TokenModel
        {
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken.Token
        };
    }
    public async Task RevokeAllRefreshTokens(Guid userId, CancellationToken cancellationToken)
    {
        var hasActiveTokens = await context.RefreshTokens
            .AnyAsync(x => x.UserId == userId && 
                           x.Revoked == null && 
                           x.Expires > DateTime.UtcNow, 
                cancellationToken);

        if (!hasActiveTokens)
            return; 
        
        await context.RefreshTokens
            .Where(x => x.UserId == userId && x.Revoked == null && x.Expires > DateTime.UtcNow)
            .ExecuteUpdateAsync(setters => setters
                    .SetProperty(x => x.Revoked, DateTime.UtcNow)
                    .SetProperty(x => x.ReplacedByToken, (string?)null),
                cancellationToken);
    }
}