using MongoDB.Driver;
using YouTube.Payment.Data.Entities;

namespace YouTube.Payment.Data.Interfaces;

public interface IMongoContext
{
    IMongoCollection<Wallet> Wallets { get; }
}