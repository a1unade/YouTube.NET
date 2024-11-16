using MediatR;
using YouTube.Application.Common.Requests.Base;
using YouTube.Application.Common.Responses.User;

namespace YouTube.Application.Features.UserRequests.GetById;

public class GetUserByIdQuery : IdRequest, IRequest<UserInfoResponse>
{
    public GetUserByIdQuery(IdRequest request) : base(request)
    {
        
    }
}