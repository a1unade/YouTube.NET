using MediatR;
using YouTube.Application.Common.Requests.User;
using YouTube.Application.Common.Responses;

namespace YouTube.Application.Features.User.ForgotPassword;

public class ForgotPasswordCommand : EmailRequest, IRequest<BaseResponse>
{
    public ForgotPasswordCommand(EmailRequest request) : base(request)
    {
        
    }
}