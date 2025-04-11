namespace YouTube.Application.Common.Requests.Payment;

public class BuyPremiumRequest
{
    public BuyPremiumRequest()
    {
        
    }
    
    public BuyPremiumRequest(BuyPremiumRequest request)
    {
        Id = request.Id;
        Price = request.Price;
    }
    
    public Guid Id { get; set; }
    
    public int Price { get; set; }
}