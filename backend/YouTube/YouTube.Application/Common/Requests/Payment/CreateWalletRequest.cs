namespace YouTube.Application.Common.Requests.Payment;

public class CreateWalletRequest
{
    
    public CreateWalletRequest()
    {
        
    }

    public CreateWalletRequest(CreateWalletRequest request)
    {
        Name = request.Name;
        UserIdPostgres = request.UserIdPostgres;
        Balance = request.Balance;
    }
    public string Name { get; set; } = default!;

    public string UserIdPostgres { get; set; } = default!;
    
    public double Balance { get; set; }
}