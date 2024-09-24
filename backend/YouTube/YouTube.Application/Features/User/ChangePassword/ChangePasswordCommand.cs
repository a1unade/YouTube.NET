using MediatR;
using YouTube.Application.Common.Requests.User;
using YouTube.Application.Common.Responses;

namespace YouTube.Application.Features.User.ChangePassword;

public class ChangePasswordCommand : ChangePasswordRequest, IRequest<BaseResponse>
{
    public ChangePasswordCommand(ChangePasswordRequest request) : base(request)
    {
        
    }
}