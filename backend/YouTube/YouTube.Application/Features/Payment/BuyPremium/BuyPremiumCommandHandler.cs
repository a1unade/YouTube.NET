using Grpc.Core;
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

namespace YouTube.Application.Features.Payment.BuyPremium;

public class BuyPremiumCommandHandler : IRequestHandler<BuyPremiumCommand, BaseResponse>
{
    private readonly IDbContext _context;
    private readonly PaymentService.PaymentServiceClient _paymentClient;
    
    public BuyPremiumCommandHandler(IDbContext context, IConfiguration configuration)
    {
        _context = context;
        var channel = GrpcChannel.ForAddress(configuration["PaymentService:GrpcEndpoint"]!);
        _paymentClient = new PaymentService.PaymentServiceClient(channel);
    }

    public async Task<BaseResponse> Handle(BuyPremiumCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .Include(u => u.Subscriptions)
            .FirstOrDefaultAsync(u => u.Id == request.Id, cancellationToken);
        
        if (user == null)
            return new BaseResponse{ Message = UserErrorMessage.UserNotFound};

        var paymentTransaction = new Transaction
        {
            Id = Guid.NewGuid(),
            Date = DateTime.UtcNow,
            Price = request.Price,
            Description = "Premium subscription payment",
            Operation = $"-{request.Price}",
            Status = PaymentStatus.Pending,
            UserId = user.Id
        };

        await _context.Transactions.AddAsync(paymentTransaction, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

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


            Console.WriteLine("Пытаемся списать средства");
            if (!paymentResponse.Success)
            {
                paymentTransaction.Status = PaymentStatus.Failed;
                await _context.SaveChangesAsync(cancellationToken);
                return new BaseResponse
                {
                    Message = paymentResponse.Error
                };
            }
            

            // Обновляем подписку
            Console.WriteLine("Обновляем подписку");
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
            Console.WriteLine("Sohranili");

            return new BaseResponse
            {
                IsSuccessfully = true,
                Message = user.Subscriptions.IsActive
                    ? $"Подписка продлена до {user.Subscriptions.EndDate:dd.MM.yyyy}"
                    : "Премиум подписка успешно активирована"
            };
        }
        catch (Exception ex)
        {
            // Откатываем изменения
            Console.WriteLine(" Откатываем изменения");
            paymentTransaction.Status = PaymentStatus.Failed;
            await _context.SaveChangesAsync(cancellationToken);

            // Пытаемся вернуть деньги (если это не таймаут)
            if (ex is not RpcException { StatusCode: StatusCode.DeadlineExceeded })
            {
                try
                {
                    Console.WriteLine("Пытаемся вернуть деньги");
                    await _paymentClient.RefundAsync(new RefundRequest
                    {
                        UserId = user.Id.ToString(),
                        Amount = (double)paymentTransaction.Price,
                        TransactionId = paymentTransaction.Id.ToString(),
                    });
                }
                catch
                {
                    return new BaseResponse { Message = "Не получилось вернуть деньги" };
                }
            }

            return ex switch
            {
                RpcException { StatusCode: StatusCode.DeadlineExceeded } =>
                    new BaseResponse { Message = "Сервис оплаты временно недоступен" },
                _ => new BaseResponse { Message = "Произошла ошибка при обработке платежа" }
            };
        }
    }
}