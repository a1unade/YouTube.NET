using MongoDB.Bson;
using MongoDB.Driver;
using YouTube.Payment.Data.Entities;
using YouTube.Payment.Data.Interfaces;

namespace YouTube.Payment.Data.Repositories;

public class WalletRepository : IWalletRepository
{
    private readonly IMongoCollection<Wallet> _wallets;

    public WalletRepository(IMongoContext context)
    {
        _wallets = context.Wallets;
        
        CreateIndexesAsync().Wait();
    }

    private async Task CreateIndexesAsync()
    {
        var indexKeys = Builders<Wallet>.IndexKeys.Ascending(x => x.UserIdPostgres);
        var indexOptions = new CreateIndexOptions { Unique = true };
        await _wallets.Indexes.CreateOneAsync(
            new CreateIndexModel<Wallet>(indexKeys, indexOptions));
    }
    
    public async Task<Wallet> CreateWalletAsync(Guid userId, string name, decimal balance, CancellationToken cancellationToken)
    {
        var wallet = new Wallet
        {
            Id = ObjectId.GenerateNewId(),
            UserName = name,
            Balance = balance,
            UserIdPostgres = userId
        };
    
        await _wallets.InsertOneAsync(wallet, cancellationToken: cancellationToken);
        return wallet;
    }

    public async Task<Wallet?> GetByIdAsync(ObjectId walletId, CancellationToken cancellationToken)
    {
        return await _wallets
            .Find(x => x.Id == walletId)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<Wallet?> GetByPostgresIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await _wallets
            .Find(x => x.UserIdPostgres == userId)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<bool> UpdateBalanceAsync(Guid userId, decimal amount, CancellationToken cancellationToken)
    {
        var filter = Builders<Wallet>.Filter.Eq(x => x.UserIdPostgres, userId);
        
        var update = Builders<Wallet>.Update
            .Inc(x => x.Balance, amount);

        var result = await _wallets.UpdateOneAsync(filter, update, cancellationToken: cancellationToken);
        return result.ModifiedCount > 0;
    }

    public async Task<bool> DeleteAsync(Guid userId, CancellationToken ct)
    {
        var result = await _wallets.DeleteOneAsync(
            x => x.UserIdPostgres == userId, 
            cancellationToken: ct);
            
        return result.DeletedCount > 0;
    }
}