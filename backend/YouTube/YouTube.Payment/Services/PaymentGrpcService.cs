using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using YouTube.Payment.Data.Entities;
using YouTube.Payment.Data.Interfaces;
using YouTube.Payment.Messages;
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
                return new PaymentResponse { Success = false, Error = WalletMessages.WalletNotFound };

            if (wallet.Balance < (decimal)request.Amount)
                return new PaymentResponse { Success = false, Error = WalletMessages.NotEnoughFounds };
            

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
            return new PaymentResponse { Success = false, Error = e.Message };
        }
    }

    public override async Task<PaymentResponse> Refund(RefundRequest request, ServerCallContext context)
    {
        try
        {
            var wallet = await _context.Wallets.FirstOrDefaultAsync(x => x.UserIdPostgres == Guid.Parse(request.UserId));

            if (wallet == null)
                return new PaymentResponse { Success = false, Error = WalletMessages.WalletNotFound };

            wallet.Balance += (decimal)request.Amount;

            await _context.SaveChangesAsync();

            return new PaymentResponse { Success = true };
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
            throw new RpcException(new Status(StatusCode.Internal, $"Failed to create wallet: {ex.Message}"));
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
                Error = WalletMessages.WalletNotFound
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
            return new WalletBalanceResponse { UserId = request.UserId, Balance = 0, Error = WalletMessages.WalletNotFound };
        

        return new WalletBalanceResponse
        {
            Success = true,
            WalletId = wallet.Id.ToString(),
            UserId = request.UserId,
            Balance = (double)wallet.Balance
        };
    }
}