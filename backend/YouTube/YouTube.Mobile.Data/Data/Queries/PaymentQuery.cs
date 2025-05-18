using Grpc.Core;
using YouTube.Proto;
using WalletBalanceResponse = YouTube.Application.Common.Responses.Payment.WalletBalanceResponse;

namespace YouTube.Mobile.Data.Data.Queries;

/// <summary>
/// 
/// </summary>
[ExtendObjectType("Query")]
public class PaymentQuery
{
    private readonly PaymentService.PaymentServiceClient _client;

    public PaymentQuery(PaymentService.PaymentServiceClient client)
    {
        _client = client;
    }
    
    [GraphQLDescription("Получить кошелек")]
    public async Task<WalletBalanceResponse?> GetUserWallet(
        [ID] Guid id, 
        CancellationToken cancellationToken
    )
    {
        try
        {
            var response = await _client.GetWalletBalanceAsync(new WalletRequest
            {
                UserId = id.ToString()
            }, cancellationToken: cancellationToken);

            if (!response.Success)
            {
                return new WalletBalanceResponse
                {
                    Message = "Не удалось"
                };
            }
            
            return new WalletBalanceResponse
            {
                IsSuccessfully = true,
                EntityId = Guid.TryParse(response.UserId, out id) ? id : Guid.Empty,
                Balance = (decimal)response.Balance,
                WalletId = Guid.TryParse(response.WalletId, out id) ? id : Guid.Empty,
            };
        }
        catch (RpcException ex) when (ex.StatusCode == StatusCode.NotFound)
        {
            throw new GraphQLException("Кошелек не найден");
        }
        catch (RpcException ex)
        {
            throw new GraphQLException($"Ошибка сервиса платежей: {ex.Status.Detail}");
        }
        catch (Exception ex)
        {
            throw new GraphQLException("Внутренняя ошибка сервера");
        }
    }
}