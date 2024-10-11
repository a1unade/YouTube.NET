using MediatR;
using YouTube.Application.Common.Requests.User;
using YouTube.Application.Common.Responses.User;

namespace YouTube.Application.Features.UserRequests.CheckUserEmail;

public class CheckUserEmailCommand : EmailRequest, IRequest<UserEmailResponse>
{
    public CheckUserEmailCommand(EmailRequest request) : base(request)
    {
        
    }
}