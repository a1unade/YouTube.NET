namespace YouTube.Application.Common.Requests.Payment;

public class UpdateBalanceRequest
{
    public UpdateBalanceRequest()
    {
        
    }

    public UpdateBalanceRequest(UpdateBalanceRequest request)
    {
        UserPostgresId = request.UserPostgresId;
        Amount = request.Amount;
        TransactionId = request.TransactionId;
    }

    public string UserPostgresId { get; set; } = default!;

    public string TransactionId { get; set; } = default!;
    
    public double Amount { get; set; }
}