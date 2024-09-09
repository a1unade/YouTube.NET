using MediatR;
using YouTube.Application.Common.Requests.Auth;
using YouTube.Application.Common.Responses;

namespace YouTube.Application.Features.Auth.Login;


public class LoginCommand : LoginRequest, IRequest<BaseResponse>
{
    public LoginCommand(LoginRequest request) : base(request)
    {
        
    }
}