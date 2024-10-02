using MediatR;
using YouTube.Application.Common.Requests.Base;
using YouTube.Application.Common.Requests.Email;
using YouTube.Application.Common.Responses;

namespace YouTube.Application.Features.User.ConfirmEmail;

public class ConfirmEmailCommand : IdRequest, IRequest<BaseResponse>
{
    public ConfirmEmailCommand(IdRequest request) : base(request)
    {
    }
}