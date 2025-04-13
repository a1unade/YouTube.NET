using MediatR;
using YouTube.Application.Common.Requests.Base;
using YouTube.Application.Common.Responses;

namespace YouTube.Application.Features.Payment.GetBalance;

public class GetBalanceQuery : IdRequest, IRequest<BaseResponse>
{
    public GetBalanceQuery(IdRequest request) : base(request)
    {
        
    }
}