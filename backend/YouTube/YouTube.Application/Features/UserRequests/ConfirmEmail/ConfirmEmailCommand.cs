using MediatR;
using YouTube.Application.Common.Requests.Base;
using YouTube.Application.Common.Responses;

namespace YouTube.Application.Features.UserRequests.ConfirmEmail;

public class ConfirmEmailCommand : IdRequest, IRequest<BaseResponse>
{
    public ConfirmEmailCommand(IdRequest request) : base(request)
    {
    }
}