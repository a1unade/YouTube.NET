using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace YouTube.Payment.Data.Entities;

public class Wallet
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public ObjectId Id { get; set; }

    public string UserName { get; set; } = default!;
    
    public decimal Balance { get; set; }
    
    public Guid UserIdPostgres { get; set; }

}