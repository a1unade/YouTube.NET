using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using YouTube.Payment.Data.Entities;
using YouTube.Payment.Data.Interfaces;
using YouTube.Proto;

namespace YouTube.Payment.Services;

public class PaymentGrpcService : PaymentService.PaymentServiceBase
{
    private readonly IPaymentContext _context;

    public PaymentGrpcService(IPaymentContext context)
    {
        _context = context;
    }

    public override async Task<PaymentResponse> Withdraw(WithdrawRequest request, ServerCallContext context)
    {
        try
        {
            var wallet = await _context.Wallets
                .FirstOrDefaultAsync(x => x.UserIdPostgres == Guid.Parse(request.UserId));

            if (wallet == null)
            {
                return new PaymentResponse { Success = false, Error = "Wallet not found" };
            }

            if (wallet.Balance < (decimal)request.Amount)
            {
                return new PaymentResponse { Success = false, Error = "Not enough funds" };
            }

            wallet.Balance -= (decimal)request.Amount;

            await _context.SaveChangesAsync();

            return new PaymentResponse
            {
                Success = true,
                UserId = wallet.UserIdPostgres.ToString(),
                TransactionId = request.TransactionId
            };
        }
        catch (Exception e)
        {
            return new PaymentResponse
            {
                Success = false,
                Error = "500 " + e.Message
            };
        }
    }

    public override async Task<PaymentResponse> Refund(RefundRequest request, ServerCallContext context)
    {
        try
        {
            var wallet =
                await _context.Wallets.FirstOrDefaultAsync(x => x.UserIdPostgres == Guid.Parse(request.UserId));

            if (wallet == null)
            {
                return new PaymentResponse { Success = false, Error = "Wallet not found" };
            }

            wallet.Balance += (decimal)request.Amount;

            await _context.SaveChangesAsync();

            return new PaymentResponse
            {
                Success = true
            };
        }
        catch (Exception e)
        {
            return new PaymentResponse
            {
                Success = false,
                UserId = request.UserId,
                TransactionId = request.TransactionId,
                Error = e.Message
            };
        }
    }

    public override async Task<WalletResponse> CreateWallet(CreateWalletRequest request, ServerCallContext context)
    {
        try
        {
            var wallet = new Wallet
            {
                Id = Guid.NewGuid(),
                UserName = request.Name,
                Balance = (decimal)request.Balance,
                UserIdPostgres = Guid.Parse(request.UserId)
            };
            
            await _context.Wallets.AddAsync(wallet);
            await _context.SaveChangesAsync();
            
            return new WalletResponse
            {
                Success = true,
                WalletId = wallet.Id.ToString(),
                UserId = wallet.UserIdPostgres.ToString()
            };
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error creating wallet for user {UserId}", request.UserId);
            Console.WriteLine(ex);
            Console.WriteLine(ex.Message);
         
            throw new RpcException(new Status(
                StatusCode.Internal,
                $"Failed to create wallet: {ex.Message}"));
        }
    }

    public override async Task<WalletResponse> TopUpWallet(TopUpRequest request, ServerCallContext context)
    {
        var wallet = await _context.Wallets
            .FirstOrDefaultAsync(x => x.UserIdPostgres == Guid.Parse(request.UserId));

        if (wallet == null)
        {
            return new WalletResponse
            {
                Success = false,
                UserId = request.UserId,
                Error = "Wallet not found"
            };
        }
        
        wallet.Balance += (decimal)request.Amount;

        await _context.SaveChangesAsync();
    
        return new WalletResponse
        {
            Success = true,
            UserId = request.UserId,
            WalletId = wallet.Id.ToString()
        };
    }

    public override async Task<WalletBalanceResponse> GetWalletBalance(WalletRequest request, ServerCallContext context)
    {
        var wallet = await _context.Wallets
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.UserIdPostgres == Guid.Parse(request.UserId));
        
        if (wallet == null)
        {
            return new WalletBalanceResponse { UserId = request.UserId, Balance = 0, Error = "Not found" };
        }

        return new WalletBalanceResponse
        {
            Success = true,
            WalletId = wallet.Id.ToString(),
            UserId = request.UserId,
            Balance = (double)wallet.Balance
        };
    }
}