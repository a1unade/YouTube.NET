using Grpc.Core;
using YouTube.Application.Common.Responses;
using YouTube.Proto;

namespace YouTube.Mobile.Data.Data.Mutations;
[ExtendObjectType("Mutation")]
public class PaymentMutation
{

    [GraphQLDescription("Creates a new wallet for the specified user with an initial balance")]
    public async Task<BaseResponse> CreateWallet(
        [ID] Guid id,
        decimal balance,
        [Service] PaymentService.PaymentServiceClient client,
        CancellationToken cancellationToken
    )
    {
        try
        {
            var response = await client.CreateWalletAsync(new CreateWalletRequest
                {
                    UserId = id.ToString(),
                    Balance = (double)balance
                },
                cancellationToken: cancellationToken);

            if (!response.Success)
            {
                return new BaseResponse
                {
                    Message = response.Error
                };
            }

            return new BaseResponse
            {
                IsSuccessfully = response.Success,
                EntityId = Guid.TryParse(response.UserId, out var userId) ? userId : null,
                Message = $"Wallet created successfully id: {response.WalletId}"
            };
        }
        catch (RpcException ex)
        {
            return new BaseResponse
            {
                Message = $"gRPC error: {ex.Status.Detail} Message: {ex.Message}"
            };
        }
    }
}