using MediatR;
using YouTube.Application.Common.Requests.Email;
using YouTube.Application.Common.Responses.User;

namespace YouTube.Application.Features.Email.CodeCheckForChangePassword;

public class CodeCheckForChangePasswordCommand : CodeCheckForChangePasswordRequest, IRequest<UserIdResponse>
{
    public CodeCheckForChangePasswordCommand(CodeCheckForChangePasswordRequest request) : base(request)
    {
        
    }
}