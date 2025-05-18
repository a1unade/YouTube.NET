using Grpc.Core;
using MediatR;
using YouTube.Application.Common.Responses;
using YouTube.Proto;

namespace YouTube.Application.Features.Payment.CreateWallet;

public class CreateWalletCommandHandler : IRequestHandler<CreateWalletCommand, BaseResponse>
{
    private readonly PaymentService.PaymentServiceClient _client;

    public CreateWalletCommandHandler(PaymentService.PaymentServiceClient client)
    {
        _client = client;
    }
    
    public async Task<BaseResponse> Handle(CreateWalletCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var response = await _client.CreateWalletAsync(new CreateWalletRequest
                {
                    UserId = request.UserIdPostgres,
                    Balance = request.Balance,
                    Name = request.Name
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
                EntityId = Guid.Parse(response.UserId)
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

