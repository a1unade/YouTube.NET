using MediatR;
using YouTube.Application.Common.Requests.Auth;
using YouTube.Application.Common.Responses;

namespace YouTube.Application.Features.Auth.Logout;

public class LogoutCommand : LogoutRequest, IRequest<BaseResponse>
{
    public LogoutCommand(LogoutRequest request) : base(request)
    {
        
    }
}