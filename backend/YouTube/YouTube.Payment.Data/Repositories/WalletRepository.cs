using Microsoft.EntityFrameworkCore;
using YouTube.Payment.Data.Entities;
using YouTube.Payment.Data.Interfaces;

namespace YouTube.Payment.Data.Repositories;

public class WalletRepository : IWalletRepository
{
    private readonly IPaymentContext _context;

    public WalletRepository(IPaymentContext context)
    {
        _context = context;
    }
    
    public async Task<Wallet> CreateWalletAsync(Guid userId, string name, decimal balance, CancellationToken cancellationToken)
    {
        var wallet = new Wallet
        {
            Id = Guid.NewGuid(),
            UserName = name,
            Balance = balance,
            UserIdPostgres = userId
        };

        await _context.Wallets.AddAsync(wallet, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        
        return wallet;
    }

    public async Task<Wallet?> GetByIdAsync(Guid walletId, CancellationToken cancellationToken)
    {
        return await _context.Wallets
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == walletId, cancellationToken);

    }

    public async Task<Wallet?> GetByPostgresIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await _context.Wallets
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.UserIdPostgres == userId, cancellationToken);
    }

    public async Task<bool> UpdateBalanceAsync(Guid userId, decimal amount, CancellationToken cancellationToken)
    {
        var wallet = await _context.Wallets
            .FirstOrDefaultAsync(x => x.UserIdPostgres == userId, cancellationToken);

        if (wallet == null)
            return false;

        wallet.Balance += amount;

        if (wallet.Balance < 0)
        {
            return false;
        }

        await _context.SaveChangesAsync(cancellationToken);
    
        return true;
    }

    public async Task<bool> DeleteAsync(Guid userId, CancellationToken ct)
    {
        var wallet = await _context.Wallets
            .FirstOrDefaultAsync(x => x.UserIdPostgres == userId, ct);

        if (wallet == null)
            return false;

        _context.Wallets.Remove(wallet);
        var affectedRows = await _context.SaveChangesAsync(ct);
        
        return affectedRows > 0;
    }
}