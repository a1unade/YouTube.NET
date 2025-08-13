using YouTube.Application.DTOs.Auth;
using YouTube.Domain.Entities;

namespace YouTube.Application.Interfaces;

public interface IJwtService
{
    Task<string> GenerateAccessToken(User user);

    Task<RefreshToken> GenerateRefreshToken(User user, CancellationToken cancellationToken);

    Task<TokenModel> RefreshTokenPair(string refreshTokenValue, CancellationToken cancellationToken);

    Task RevokeAllRefreshTokens(Guid userId, CancellationToken cancellationToken);
}