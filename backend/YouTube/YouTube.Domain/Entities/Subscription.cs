using System.ComponentModel.DataAnnotations.Schema;

namespace YouTube.Domain.Entities;

public class Subscription
{
    public int Id { get; set; }

    public Guid UserId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }
    public decimal Amount { get; set; }
    
    public bool IsActive { get; set; }
    
    [ForeignKey(nameof(UserId))]
    [InverseProperty(nameof(User.Subscriptions))]
    public User User { get; set; }
}