using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using YouTube.Application.Interfaces;
/*
namespace YouTube.Infrastructure.Services;

public class SubscriptionTimer : IHostedService
{
    private readonly ISubscriptionService _subscriptionService;
    private readonly IEmailService _emailService;
    private readonly ILogger<SubscriptionTimer> _logger;

    public SubscriptionTimer(ISubscriptionService subscriptionService, IEmailService emailService, ILogger<SubscriptionTimer> logger)
    {
        _subscriptionService = subscriptionService;
        _emailService = emailService;
        _logger = logger;
    }

    public void Start()
    {
        // Настраиваем таймер на отправку email-уведомлений о скором окончании подписки каждый день
        var timer = new Timer(async _ =>
        {
            var subscriptions = await _subscriptionService.GetSubscriptionsAsync();

            foreach (var subscription in subscriptions)
            {
                if (subscription.EndDate?.Subtract(DateTime.Now).TotalDays <= 3)
                {
                    await _emailService.SendEmailAsync(subscription.User.Email,"Окончание подписки ", "Окончание подписки");
                }
            }

        }, null, TimeSpan.Zero, TimeSpan.FromDays(1));
    }
}*/