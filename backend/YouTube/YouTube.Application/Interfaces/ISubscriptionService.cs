using System.Collections;
using YouTube.Application.Common.Responses;
using YouTube.Application.DTOs.Auth;
using YouTube.Domain.Entities;

namespace YouTube.Application.Interfaces;

public interface ISubscriptionService
{
    Task<UserResponse> SubscribeAsync(SubscriptionDto subscriptionDto);
    Task<UserResponse> CancelSubscriptionAsync(int subscriptionId);
    Task<List<Subscription>>  GetSubscriptionsAsync();
}