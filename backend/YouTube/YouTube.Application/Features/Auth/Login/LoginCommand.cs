using MediatR;
using YouTube.Application.Common.Requests.Auth;
using YouTube.Application.Common.Responses.Auth;

namespace YouTube.Application.Features.Auth.Login;


public class LoginCommand : LoginRequest, IRequest<AuthResponse>
{
    public LoginCommand(LoginRequest request) : base(request)
    {
        
    }
}