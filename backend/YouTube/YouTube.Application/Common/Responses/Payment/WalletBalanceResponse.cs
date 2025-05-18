namespace YouTube.Application.Common.Responses.Payment;

public class WalletBalanceResponse : BaseResponse
{
    public Guid WalletId { get; set; }
    public decimal Balance { get; set; }
    
}