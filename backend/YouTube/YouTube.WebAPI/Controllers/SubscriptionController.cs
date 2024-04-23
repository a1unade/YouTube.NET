using Microsoft.AspNetCore.Mvc;
using YouTube.Application.Common.Enums;
using YouTube.Application.Common.Responses;
using YouTube.Application.DTOs.Auth;
using YouTube.Application.Interfaces;

namespace YouTube.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SubscriptionController : ControllerBase
{
    private readonly ISubscriptionService _subscriptionService;

    public SubscriptionController(ISubscriptionService subscriptionService)
    {
        _subscriptionService = subscriptionService;
    }

    [HttpPost]
    public async Task<IActionResult> Subscribe(SubscriptionDto subscriptionDto)
    {
        UserResponse result = await _subscriptionService.SubscribeAsync(subscriptionDto);

        if (result.Type == UserResponseTypes.Success)
            return Ok(result);

        return BadRequest(result.Message);
    }

    [HttpDelete("{subscriptionId}")]
    public async Task<IActionResult> CancelSubscription(int subscriptionId)
    {
        UserResponse result = await _subscriptionService.CancelSubscriptionAsync(subscriptionId);

        if (result.Type == UserResponseTypes.Success)
            return Ok(result);

        return BadRequest(result.Message);
    }
}
