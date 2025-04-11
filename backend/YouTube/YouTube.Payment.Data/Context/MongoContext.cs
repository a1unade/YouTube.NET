using MongoDB.Driver;
using YouTube.Payment.Data.Entities;
using YouTube.Payment.Data.Interfaces;
using YouTube.Payment.Data.Options;

namespace YouTube.Payment.Data.Context;

public class MongoContext : IMongoContext
{
    private readonly IMongoDatabase _database;

    public MongoContext(IMongoClient mongoClient)
    {
        _database = mongoClient.GetDatabase("youtube-payments");
    }

    public IMongoCollection<Wallet> Wallets => 
        _database.GetCollection<Wallet>("wallets");
}