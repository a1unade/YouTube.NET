using MediatR;
using YouTube.Application.Common.Requests.Payment;
using YouTube.Application.Common.Responses;

namespace YouTube.Application.Features.Payment.CreateWallet;

public class CreateWalletCommand : CreateWalletRequest, IRequest<BaseResponse>
{
    public CreateWalletCommand(CreateWalletRequest request) : base(request)
    {
        
    }
}