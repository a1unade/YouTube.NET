using MediatR;
using YouTube.Application.Common.Requests.Base;
using YouTube.Application.Common.Requests.User;
using YouTube.Application.Common.Responses;

namespace YouTube.Application.Features.UserRequests.ConfirmEmail;

public class ConfirmEmailCommand : EmailRequest, IRequest<BaseResponse>
{
    public ConfirmEmailCommand(EmailRequest request) : base(request)
    {
    }
}