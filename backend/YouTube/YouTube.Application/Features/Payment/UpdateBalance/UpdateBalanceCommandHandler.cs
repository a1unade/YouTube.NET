using Grpc.Net.Client;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using YouTube.Application.Common.Messages.Error;
using YouTube.Application.Common.Responses;
using YouTube.Application.Interfaces;
using YouTube.Domain.Common;
using YouTube.Domain.Entities;
using YouTube.Proto;

namespace YouTube.Application.Features.Payment.UpdateBalance;

public class UpdateBalanceCommandHandler : IRequestHandler<UpdateBalanceCommand, BaseResponse>
{
    private readonly IDbContext _context;
    private readonly PaymentService.PaymentServiceClient _client;

    public UpdateBalanceCommandHandler(IConfiguration configuration, IDbContext context)
    {
        _context = context;
        var channel = GrpcChannel.ForAddress(configuration["PaymentService:GrpcEndpoint"]!);
        _client = new PaymentService.PaymentServiceClient(channel);
    }
    
    public async Task<BaseResponse> Handle(UpdateBalanceCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(x => x.Id == Guid.Parse(request.UserPostgresId), cancellationToken);

        if (user == null)
            return new BaseResponse(false, UserErrorMessage.UserNotFound);

        var transaction = new Transaction
        {
            Id = Guid.NewGuid(),
            Date = DateTime.UtcNow,
            Price = (decimal)request.Amount,
            Description = $"+{request.Amount}",
            Operation = $"Update balance +{request.Amount} for: {request.UserPostgresId}",
            Status = PaymentStatus.Pending,
            UserId = user.Id
        };

        await _context.Transactions.AddAsync(transaction, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        try
        {
            var response = await _client.TopUpWalletAsync(new TopUpRequest
            {
                UserId = request.UserPostgresId,
                Amount = request.Amount
            }, cancellationToken: cancellationToken);

            if (!response.Success)
            {
                transaction.Status = PaymentStatus.Failed;
                await _context.SaveChangesAsync(cancellationToken);
                
                return new BaseResponse(false, response.Error);
            }

            transaction.Status = PaymentStatus.Completed;
            await _context.SaveChangesAsync(cancellationToken);

            return new BaseResponse(true, "Balance updated successfully")
            {
                EntityId = Guid.Parse(response.UserId)
            };
        }
        catch (Exception ex)
        {
            transaction.Status = PaymentStatus.Failed;
            await _context.SaveChangesAsync(cancellationToken);

            return new BaseResponse(false, $"Failed to update balance: {ex.Message}");
        }
    }
    
}

