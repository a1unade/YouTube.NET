using Grpc.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using YouTube.Application.Common.Messages.Error;
using YouTube.Application.Common.Responses;
using YouTube.Application.Interfaces;
using YouTube.Domain.Common;
using YouTube.Domain.Entities;
using YouTube.Proto;

namespace YouTube.Application.Features.Payment.BuyPremium;

public class BuyPremiumCommandHandler : IRequestHandler<BuyPremiumCommand, BaseResponse>
{
    private readonly IDbContext _context;
    private readonly PaymentService.PaymentServiceClient _paymentClient;
    private readonly ILogger<BuyPremiumCommandHandler> _logger;

    
    public BuyPremiumCommandHandler(IDbContext context, PaymentService.PaymentServiceClient client, ILogger<BuyPremiumCommandHandler> logger)
    {
        _context = context;
       _paymentClient = client;
       _logger = logger;
    }

    public async Task<BaseResponse> Handle(BuyPremiumCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .Include(u => u.Subscriptions)
            .FirstOrDefaultAsync(u => u.Id == request.Id, cancellationToken);
        
        if (user == null)
            return new BaseResponse{ Message = UserErrorMessage.UserNotFound};

        
        var transactionId = Guid.NewGuid();
        var paymentTransaction = new Transaction
        {
            Id = transactionId,
            Date = DateTime.UtcNow,
            Price = request.Price,
            Description = "Premium subscription payment",
            Operation = $"-{request.Price}",
            Status = PaymentStatus.Pending,
            UserId = user.Id
        };

        await _context.Transactions.AddAsync(paymentTransaction, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        
        await using var dbTransaction = await _context.BeginTransactionAsync(cancellationToken);
        
        try
        {
            var paymentResponse = await _paymentClient.WithdrawAsync(
                new WithdrawRequest
                {
                    UserId = user.Id.ToString(),
                    Amount = request.Price,
                    TransactionId = paymentTransaction.Id.ToString()
                },
                cancellationToken: cancellationToken);

            if (!paymentResponse.Success)
            {
                await dbTransaction.RollbackAsync(cancellationToken);
                paymentTransaction.Status = PaymentStatus.Failed;
                await _context.SaveChangesAsync(cancellationToken);
                
                return new BaseResponse { Message = paymentResponse.Error };
            }
            
            if (user.Subscriptions == null)
            {
                user.Subscriptions = new Premium
                {
                    Id = Guid.NewGuid(),
                    StartDate = DateTime.UtcNow,
                    EndDate = DateTime.UtcNow.AddMonths(1),
                    Price = request.Price,
                    IsActive = true,
                    UserId = user.Id
                };
                await _context.Premiums.AddAsync(user.Subscriptions, cancellationToken);
            }
            else
            {
                user.Subscriptions.EndDate = user.Subscriptions.EndDate > DateTime.UtcNow
                    ? user.Subscriptions.EndDate.Value.AddMonths(1)
                    : DateTime.UtcNow.AddMonths(1);
                
                user.Subscriptions.IsActive = true;
                user.Subscriptions.Price = request.Price;
            }

            paymentTransaction.Status = PaymentStatus.Completed;
            await _context.SaveChangesAsync(cancellationToken);

            await dbTransaction.CommitAsync(cancellationToken);

            return new BaseResponse
            {
                IsSuccessfully = true,
                EntityId = user.Id,
                Message = user.Subscriptions.IsActive
                    ? $"Подписка продлена до {user.Subscriptions.EndDate:dd.MM.yyyy}"
                    : "Премиум подписка успешно активирована"
            };
        }
        catch (Exception ex)
        {
            try
            {
                await dbTransaction.RollbackAsync(cancellationToken);
                paymentTransaction.Status = PaymentStatus.Failed;
                await _context.SaveChangesAsync(cancellationToken);

                if (ex is not RpcException { StatusCode: StatusCode.DeadlineExceeded })
                {
                    await CompensateWithdrawal(user.Id, transactionId, request.Price);
                }

                return ex switch
                {
                    RpcException { StatusCode: StatusCode.DeadlineExceeded } =>
                        new BaseResponse { Message = "Сервис оплаты временно недоступен" },
                    _ => new BaseResponse { Message = "Произошла ошибка при обработке платежа" }
                };
            }
            catch (Exception compensationEx)
            {
                _logger.LogError(compensationEx, "Ошибка при компенсации транзакции");
                return new BaseResponse { Message = "Ошибка при отмене операции" };
            }
        }
    }
    private async Task CompensateWithdrawal(Guid userId, Guid transactionId, decimal amount)
    {
        try
        {
            await _paymentClient.RefundAsync(new RefundRequest
            {
                UserId = userId.ToString(),
                Amount = (double)amount,
                TransactionId = transactionId.ToString()
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при возврате средств");
            throw;
        }
    }
}