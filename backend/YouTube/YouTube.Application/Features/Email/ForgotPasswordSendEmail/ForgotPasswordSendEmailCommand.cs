using MediatR;
using YouTube.Application.Common.Requests.User;
using YouTube.Application.Common.Responses;

namespace YouTube.Application.Features.Email.ForgotPasswordSendEmail;

public class ForgotPasswordSendEmailCommand : EmailRequest, IRequest<BaseResponse>
{
    public ForgotPasswordSendEmailCommand(EmailRequest request) : base(request)
    {
        
    }
}