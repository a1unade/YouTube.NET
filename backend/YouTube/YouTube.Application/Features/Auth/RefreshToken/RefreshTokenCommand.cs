using MediatR;
using YouTube.Application.Common.Requests.Auth;
using YouTube.Application.Common.Responses.Auth;

namespace YouTube.Application.Features.Auth.RefreshToken;

public class RefreshTokenCommand : RefreshTokenRequest, IRequest<RefreshTokenResponse>
{
    public RefreshTokenCommand(RefreshTokenRequest request) : base(request)
    {
        
    }
}