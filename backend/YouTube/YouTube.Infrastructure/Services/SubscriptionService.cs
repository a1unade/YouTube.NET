using Microsoft.EntityFrameworkCore;
using YouTube.Application.Common.Enums;
using YouTube.Application.Common.Responses;
using YouTube.Application.DTOs.Auth;
using YouTube.Application.Interfaces;
using YouTube.Domain.Entities;
using YouTube.Persistence.Contexts;

namespace YouTube.Infrastructure.Services;

public class SubscriptionService : ISubscriptionService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IEmailService _emailService;

    public SubscriptionService(ApplicationDbContext dbContext, IEmailService emailService)
    {
        _dbContext = dbContext;
        _emailService = emailService;
    }

    public async Task<AuthResponse> SubscribeAsync(SubscriptionDto subscriptionDto)
    {
        var subscription = new Subscription
        {
            UserId = subscriptionDto.UserId,
            StartDate = subscriptionDto.StartDate,
            EndDate = subscriptionDto.EndDate,
            Amount = subscriptionDto.Amount,
            IsActive = true
        };
        
        await _dbContext.Subscriptions.AddAsync(subscription);
        await _dbContext.SaveChangesAsync();
        await _emailService.SendEmailAsync(subscription.User.Email,"Подтверждение почты", $"{subscription.Amount}");
        

        return new AuthResponse { Type = UserResponseTypes.Success, Message = "Подписка успешно оформлена" };
    }

    public async Task<AuthResponse> CancelSubscriptionAsync(int subscriptionId)
    {
        var subscription = await _dbContext.Subscriptions.FindAsync(subscriptionId);

        if (subscription == null)
            return new AuthResponse { Type = UserResponseTypes.Error, Message = "Подписка не найдена" };
        
        subscription.IsActive = false;
        await _dbContext.SaveChangesAsync();

        return new AuthResponse { Type = UserResponseTypes.Success, Message = "Подписка успешно отменена" };
    }
    public async Task<List<Subscription>> GetSubscriptionsAsync()
    {
        return await _dbContext.Subscriptions.ToListAsync();
    }
}