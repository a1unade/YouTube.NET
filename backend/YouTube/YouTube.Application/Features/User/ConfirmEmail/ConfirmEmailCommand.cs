using MediatR;
using YouTube.Application.Common.Requests.Email;
using YouTube.Application.Common.Responses;

namespace YouTube.Application.Features.User.ConfirmEmail;

public class ConfirmEmailCommand : EmailConfirmRequest, IRequest<BaseResponse>
{
    public ConfirmEmailCommand(EmailConfirmRequest request) : base(request)
    {
    }
}