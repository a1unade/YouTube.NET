namespace YouTube.Payment.Data.Options;

public class MongoDbSettings
{
    public string ConnectionString { get; set; } = default!;
    
    public string DatabaseName { get; set; } = default!;
}