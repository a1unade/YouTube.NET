using MediatR;
using YouTube.Application.Common.Requests.Email;
using YouTube.Application.Common.Responses;

namespace YouTube.Application.Features.EmailFeatures.CodeCheck;

public class CodeCheckCommand : CodeCheckRequest, IRequest<BaseResponse>
{
    public CodeCheckCommand(CodeCheckRequest request) : base(request)
    {
    }
}