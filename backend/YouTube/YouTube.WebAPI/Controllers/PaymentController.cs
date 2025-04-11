using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YouTube.Application.Common.Requests.Base;
using YouTube.Application.Common.Requests.Payment;
using YouTube.Application.Interfaces;
using YouTube.Domain.Common;
using YouTube.Domain.Entities;
using YouTube.Proto;
using CreateWalletRequest = YouTube.Application.Common.Requests.Payment.CreateWalletRequest;

namespace YouTube.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class PaymentController : ControllerBase
{
    private readonly IDbContext _context;
    private readonly PaymentService.PaymentServiceClient _paymentClient;
    
    public PaymentController(IDbContext context, PaymentService.PaymentServiceClient paymentClient)
    {
        _context = context;
        var channel = GrpcChannel.ForAddress("http://localhost:8085");
        _paymentClient = new PaymentService.PaymentServiceClient(channel);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> BuyPremium(BuyPremiumRequest request, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .Include(u => u.Subscriptions)
            .FirstOrDefaultAsync(u => u.Id == request.Id, cancellationToken);

        if (user == null)
            return new NotFoundResult();

        bool paymentWithdrawn = false;

        var transaction = new Transaction
        {
            Id = Guid.NewGuid(),
            Date = DateTime.UtcNow,
            Price = request.Price,
            Description = "Premium subscription payment",
            Operation = $"-{request.Price}",
            Status = PaymentStatus.Pending,
            UserId = user.Id
        };

        var originalSubscription = user.Subscriptions != null
            ? new Premium
            {
                Id = user.Subscriptions.Id,
                StartDate = user.Subscriptions.StartDate,
                EndDate = user.Subscriptions.EndDate,
                Price = user.Subscriptions.Price,
                IsActive = user.Subscriptions.IsActive
            }
            : null;

        await _context.Transactions.AddAsync(transaction, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        try
        {
            var paymentResponse = await _paymentClient.WithdrawAsync(
                new WithdrawRequest
                {
                    UserId = user.Id.ToString(),
                    Amount = request.Price,
                    TransactionId = transaction.Id.ToString()
                },
                cancellationToken: cancellationToken);

            if (!paymentResponse.Success)
                return NotFound(new
                {
                    Message = paymentResponse.Error 
                });

            paymentWithdrawn = true;

            BuyOrExtendPremium(user, request.Price);
            await _context.SaveChangesAsync(cancellationToken);

            transaction.Status = PaymentStatus.Completed;
            await _context.SaveChangesAsync(cancellationToken);

            return Ok(new
            {
                IsSuccessfully = true, Message = user.Subscriptions!.IsActive
                    ? $"Подписка продлена до {user.Subscriptions.EndDate:dd.MM.yyyy}"
                    : "Премиум подписка успешно активирована"
            });
         
        }
        catch (RpcException ex) when (ex.StatusCode == Grpc.Core.StatusCode.DeadlineExceeded)
        {
            await TryRollbackAsync(user, originalSubscription, transaction, paymentWithdrawn);
            return NotFound(new
            {
                Message = "Сервис оплаты временно недоступен"   
            });
        }
        catch (Exception ex)
        {
            await TryRollbackAsync(user, originalSubscription, transaction, paymentWithdrawn);
            return NotFound(new
            {
                Message = $"Ошибка: {ex.Message}"
            });
        }
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> CreateWallet([FromBody] CreateWalletRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var response = await _paymentClient.CreateWalletAsync(
                new Proto.CreateWalletRequest
                {
                    UserId = request.UserIdPostgres,
                    Balance = request.Balance,
                    Name = request.Name
                },
                cancellationToken: cancellationToken);

            if (!response.Success)
            {
                return BadRequest(response.Error);
            }

            return Ok(response);
        }
        catch (RpcException ex)
        {
            Console.WriteLine($"gRPC error: {ex.Status.Detail}");
            return StatusCode(500, $"gRPC error: {ex.Status.Detail}");
        }
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> UpdateBalance(UpdateBalanceRequest request, CancellationToken cancellationToken)
    {
        var response = await _paymentClient.TopUpWalletAsync(new TopUpRequest
        {
            UserId = request.UserPostgresId,
            Amount = request.Amount
        }, cancellationToken: cancellationToken);

        return Ok(response);
    }
    
    [HttpGet("[action]")]
    public async Task<IActionResult> GetBalance(IdRequest request, CancellationToken cancellationToken)
    {
        var response = await _paymentClient.GetWalletBalanceAsync(new WalletRequest
        {
            UserId = request.Id.ToString()
        }, cancellationToken: cancellationToken);

        return Ok(response);
    }
    
    
    private async Task TryRollbackAsync(User user, Premium? originalSubscription, Transaction? transaction, bool paymentWithdrawn)
    {
        try
        {
            if (originalSubscription != null)
            {
                user.Subscriptions = originalSubscription;
            }
            else
            {
                user.Subscriptions = null;
            }

            if (paymentWithdrawn && transaction != null)
            {
                await _paymentClient.RefundAsync(new RefundRequest
                {
                    UserId = user.Id.ToString(),
                    Amount = (double)transaction.Price,
                    TransactionId = transaction.Id.ToString(),
                });
            }

            if (transaction != null)
            {
                _context.Transactions.Remove(transaction);
            }

            await _context.SaveChangesAsync();
        }
        catch (Exception)
        {
            Console.WriteLine("Пизда вообще");
        }
    }

    private void BuyOrExtendPremium(User user, int price)
    {
        if (user.Subscriptions == null)
        {
            user.Subscriptions = new Premium
            {
                Id = Guid.NewGuid(),
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddMonths(1),
                Price = price,
                IsActive = true,
                UserId = user.Id
            };
        }
        else
        {
            var currentEndDate = user.Subscriptions.EndDate > DateTime.UtcNow
                ? user.Subscriptions.EndDate.Value
                : DateTime.UtcNow;

            user.Subscriptions.EndDate = currentEndDate.AddMonths(1);
            user.Subscriptions.IsActive = true;
            user.Subscriptions.Price = price;
        }
    }
}

