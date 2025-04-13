using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
    private readonly ILogger<UpdateBalanceCommandHandler> _logger;

    public UpdateBalanceCommandHandler(IDbContext context, PaymentService.PaymentServiceClient client, ILogger<UpdateBalanceCommandHandler> logger)
    {
        _context = context;
        _client = client;
        _logger = logger;
    }
    
    public async Task<BaseResponse> Handle(UpdateBalanceCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(x => x.Id == Guid.Parse(request.UserPostgresId), cancellationToken);

        if (user == null)
            return new BaseResponse(false, UserErrorMessage.UserNotFound);

        var transactionId = Guid.NewGuid();
        var transaction = new Transaction
        {
            Id = transactionId,
            Date = DateTime.UtcNow,
            Price = (decimal)request.Amount,
            Description = $"+{request.Amount}",
            Operation = $"Update balance +{request.Amount} for: {request.UserPostgresId}",
            Status = PaymentStatus.Pending,
            UserId = user.Id
        };

        await _context.Transactions.AddAsync(transaction, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        
        await using var dbTransaction = await _context.BeginTransactionAsync(cancellationToken);

        try
        {
            var response = await _client.TopUpWalletAsync(new TopUpRequest
            {
                UserId = request.UserPostgresId,
                Amount = request.Amount
            }, cancellationToken: cancellationToken);

            if (!response.Success)
            {
                await dbTransaction.RollbackAsync(cancellationToken);
                transaction.Status = PaymentStatus.Failed;
                await _context.SaveChangesAsync(cancellationToken);
                
                return new BaseResponse(false, response.Error);
            }

            transaction.Status = PaymentStatus.Completed;
            await _context.SaveChangesAsync(cancellationToken);
            await dbTransaction.CommitAsync(cancellationToken);

            return new BaseResponse(true, "Balance updated successfully")
            {
                EntityId = Guid.Parse(response.UserId)
            };
        }
        catch (Exception ex)
        {
            try
            {
                await dbTransaction.RollbackAsync(cancellationToken);
                transaction.Status = PaymentStatus.Failed;
                await _context.SaveChangesAsync(cancellationToken);

                await CompensateTopUp(Guid.Parse(request.UserPostgresId), transactionId, request.Amount);
                
                return new BaseResponse(false, $"Failed to update balance: {ex.Message}");
            }
            catch (Exception compensationEx)
            {
                _logger.LogError(compensationEx, "Ошибка при компенсации пополнения баланса");
                return new BaseResponse(false, "Ошибка при отмене операции пополнения");
            }
        }
    }
    private async Task CompensateTopUp(Guid userId, Guid transactionId, double amount)
    {
        try
        {
            var compensateResponse = await _client.TopUpWalletAsync(new TopUpRequest
            {
                UserId = userId.ToString(),
                Amount = -amount, 
                TransactionId = transactionId.ToString()
            });

            if (!compensateResponse.Success)
            {
                _logger.LogError($"Не удалось компенсировать пополнение: {compensateResponse.Error}");
            }
            
            _logger.LogInformation($"Компенсация пополнения для пользователя {userId}, транзакция {transactionId}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при компенсации пополнения баланса");
            throw;
        }
    }
}

