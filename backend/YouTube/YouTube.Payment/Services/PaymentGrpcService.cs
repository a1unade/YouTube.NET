using Grpc.Core;
using MongoDB.Driver;
using YouTube.Payment.Data.Entities;
using YouTube.Payment.Data.Interfaces;
using YouTube.Payment.Protos;

namespace YouTube.Payment.Services;

public class PaymentGrpcService : PaymentService.PaymentServiceBase
{
    private readonly IWalletRepository _walletRepository;
    private readonly IMongoCollection<Wallet> _wallets;

    public PaymentGrpcService(IMongoDatabase db, IWalletRepository walletRepository)
    {
        _walletRepository = walletRepository;
        _wallets = db.GetCollection<Wallet>("wallets");
    }

    public override async Task<PaymentResponse> Withdraw(WithdrawRequest request, ServerCallContext context)
    {
        try
        {
            var wallet = await _wallets
                .Find(w => w.UserIdPostgres == Guid.Parse(request.UserId))
                .FirstOrDefaultAsync();

            if (wallet == null)
            {
                return new PaymentResponse { Success = false, Error = "Wallet not found" };
            }

            if (wallet.Balance < (decimal)request.Amount)
            {
                return new PaymentResponse { Success = false, Error = "Not enough funds" };
            }

            wallet.Balance -= (decimal)request.Amount;
            await _wallets.ReplaceOneAsync(w => w.Id == wallet.Id, wallet);

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
                Error = "500 " + e.Message
            };
        }
    }

    public override async Task<PaymentResponse> Refund(RefundRequest request, ServerCallContext context)
    {
        try
        {
            var wallet = await _wallets
                .Find(w => w.UserIdPostgres == Guid.Parse(request.UserId))
                .FirstOrDefaultAsync();

            if (wallet == null)
            {
                return new PaymentResponse { Success = false, Error = "Wallet not found" };
            }

            wallet.Balance += (decimal)request.Amount;

            await _wallets.ReplaceOneAsync(w => w.Id == wallet.Id, wallet);

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
        var wallet = await _walletRepository.CreateWalletAsync(
            Guid.Parse(request.UserId),
            request.Name,
            (decimal)request.Balance);

        return new WalletResponse
        {
            Success = true,
            WalletId = wallet.Id.ToString(),
            UserId = wallet.UserIdPostgres.ToString()
        };
    }

    public override async Task<WalletResponse> TopUpWallet(TopUpRequest request, ServerCallContext context)
    {
        var result = await _walletRepository.UpdateBalanceAsync(Guid.Parse(request.UserId), (decimal)request.Amount);

        return new WalletResponse
        {
            Success = result,
            UserId = request.UserId
        };
    }

    public override async Task<WalletBalanceResponse> GetWalletBalance(WalletRequest request, ServerCallContext context)
    {
        var wallet = await _walletRepository.GetByPostgresIdAsync(Guid.Parse(request.UserId));

        if (wallet == null)
        {
            return new WalletBalanceResponse { WalletId = null, UserId = null, Balance = 0, Error = "Not found" };
        }
        
        return new WalletBalanceResponse
        {
            Success = true,
            WalletId = wallet.Id.ToString(),
            UserId = wallet.UserIdPostgres.ToString(),
            Balance = (double)wallet.Balance,
        };
    }
}