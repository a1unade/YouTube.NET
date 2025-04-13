using MediatR;
using YouTube.Application.Common.Requests.Payment;
using YouTube.Application.Common.Responses;

namespace YouTube.Application.Features.Payment.UpdateBalance;

public class UpdateBalanceCommand : UpdateBalanceRequest, IRequest<BaseResponse>
{
    public UpdateBalanceCommand(UpdateBalanceRequest request) : base(request)
    {
        
    }
}