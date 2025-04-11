using MediatR;
using YouTube.Application.Common.Requests.Payment;
using YouTube.Application.Common.Responses;

namespace YouTube.Application.Features.Payment.BuyPremium;

public class BuyPremiumCommand : BuyPremiumRequest, IRequest<BaseResponse>
{
    public BuyPremiumCommand(BuyPremiumRequest request) : base(request)
    {

    }
}