using MediatR;
using YouTube.Application.Common.Requests.Auth;
using YouTube.Application.Common.Responses.Auth;

namespace YouTube.Application.Features.Auth.Auth;

public class AuthCommand : AuthRequest, IRequest<AuthResponse>
{
    public AuthCommand(AuthRequest request) : base(request)
    {
    }
}