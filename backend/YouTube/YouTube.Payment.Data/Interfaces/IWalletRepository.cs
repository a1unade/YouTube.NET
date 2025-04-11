using MongoDB.Bson;
using YouTube.Payment.Data.Entities;

namespace YouTube.Payment.Data.Interfaces;

public interface IWalletRepository
{
    Task<Wallet> CreateWalletAsync(Guid userId, string name, decimal balance,
        CancellationToken cancellationToken = default);

    Task<Wallet?> GetByIdAsync(ObjectId walletId, CancellationToken cancellationToken = default);

    Task<Wallet?> GetByPostgresIdAsync(Guid userId, CancellationToken cancellationToken = default);

    Task<bool> UpdateBalanceAsync(Guid walletId, decimal amount, CancellationToken cancellationToken = default);

    Task<bool> DeleteAsync(Guid walletId, CancellationToken cancellationToken = default);
}