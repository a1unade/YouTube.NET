namespace YouTube.Payment.Data.Entities;

public class Wallet
{
    public Guid Id { get; set; }
    public string UserName { get; set; } = null!;
    
    public decimal Balance { get; set; }
    
    public Guid UserIdPostgres { get; set; }

}