using MediatR;
using YouTube.Application.Common.Responses.Auth;
using YouTube.Application.Interfaces;

namespace YouTube.Application.Features.Auth.RefreshToken;

public class RefreshTokenHandler : IRequestHandler<RefreshTokenCommand, RefreshTokenResponse>
{
    private readonly IJwtService _jwtService;

    public RefreshTokenHandler(IJwtService jwtService)
    {
        _jwtService = jwtService;
    }
    
    public async Task<RefreshTokenResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var tokenPair = await _jwtService.RefreshTokenPair(request.RefreshToken, cancellationToken);

        return new RefreshTokenResponse
        {
            IsSuccessfully = true,
            Message = "Refresh token successfully.",
            EntityId = null,
            Token = tokenPair.AccessToken,
            RefreshToken = tokenPair.RefreshToken
        };
    }
}