using MediatR;
using YouTube.Application.Common.Requests.Email;
using YouTube.Application.Common.Responses;

namespace YouTube.Application.Features.Email.CodeCheckForConfirmEmail;

public class CodeCheckForConfirmEmailCommand : CodeCheckRequest, IRequest<BaseResponse>
{
    public CodeCheckForConfirmEmailCommand(CodeCheckRequest request) : base(request)
    {
    }
}