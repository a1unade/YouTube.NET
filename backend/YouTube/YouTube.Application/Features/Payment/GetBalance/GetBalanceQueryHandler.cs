using System.Globalization;
using MediatR;
using YouTube.Application.Common.Responses;
using YouTube.Proto;

namespace YouTube.Application.Features.Payment.GetBalance;

public class GetBalanceQueryHandler : IRequestHandler<GetBalanceQuery, BaseResponse>
{
    private readonly PaymentService.PaymentServiceClient _client;

    public GetBalanceQueryHandler(PaymentService.PaymentServiceClient client)
    {
        _client = client;
    }
    
    public async Task<BaseResponse> Handle(GetBalanceQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var response = await _client.GetWalletBalanceAsync(new WalletRequest
            {
                UserId = request.Id.ToString()
            }, cancellationToken: cancellationToken);

            if (!response.Success)
            {
                return new BaseResponse(false, "Не удалось");
            }
            
            return new BaseResponse
            {
                IsSuccessfully = true,
                Message = response.Balance.ToString(CultureInfo.CurrentCulture),
                EntityId = Guid.Parse(response.UserId)
            };
        }
        catch (Exception)
        {
            return new BaseResponse(false, "Сервис временно не доступен");
        }
    }
}