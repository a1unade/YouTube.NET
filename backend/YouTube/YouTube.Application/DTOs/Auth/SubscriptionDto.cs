namespace YouTube.Application.DTOs.Auth;

public class SubscriptionDto
{
    public Guid UserId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    
    public decimal Amount { get; set; }
}