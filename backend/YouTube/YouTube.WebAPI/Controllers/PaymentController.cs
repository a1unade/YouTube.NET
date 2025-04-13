using MediatR;
using Microsoft.AspNetCore.Mvc;
using YouTube.Application.Common.Requests.Base;
using YouTube.Application.Common.Requests.Payment;
using YouTube.Application.Features.Payment.BuyPremium;
using YouTube.Application.Features.Payment.CreateWallet;
using YouTube.Application.Features.Payment.GetBalance;
using YouTube.Application.Features.Payment.UpdateBalance;
using CreateWalletRequest = YouTube.Application.Common.Requests.Payment.CreateWalletRequest;

namespace YouTube.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class PaymentController : ControllerBase
{
    private readonly IMediator _mediator;


    public PaymentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Создать пользователю кошелек
    /// </summary>
    /// <param name="request">CreateWalletRequest</param>
    /// <param name="cancellationToken">cancellationToken</param>
    /// <returns>Создали кошелек</returns>
    [HttpPost("[action]")]
    public async Task<IActionResult> CreateWallet([FromBody] CreateWalletRequest request,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new CreateWalletCommand(request), cancellationToken);

        if (!response.IsSuccessfully)
            return BadRequest(response);

        return Ok(response);
    }

    /// <summary>
    /// Пополнить кошелек
    /// </summary>
    /// <param name="request">UpdateBalanceRequest</param>
    /// <param name="cancellationToken">cancellationToken</param>
    /// <returns></returns>
    [HttpPost("[action]")]
    public async Task<IActionResult> UpdateBalance(UpdateBalanceRequest request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new UpdateBalanceCommand(request), cancellationToken);

        if (!response.IsSuccessfully)
            return BadRequest(response);

        return Ok(response);
    }

    /// <summary>
    /// Получить кошелек 
    /// </summary>
    /// <param name="request">IdRequest</param>
    /// <param name="cancellationToken">cancellationToken</param>
    /// <returns></returns>
    [HttpGet("[action]")]
    public async Task<IActionResult> GetBalance([FromQuery] IdRequest request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetBalanceQuery(request), cancellationToken);

        if (!response.IsSuccessfully)
            return BadRequest(response);
        
        return Ok(response);
    }

    /// <summary>
    /// Купить премиум подписку 
    /// </summary>
    /// <param name="request">BuyPremiumRequest</param>
    /// <param name="cancellationToken">cancellationToken</param>
    /// <returns></returns>
    [HttpPost("[action]")]
    public async Task<IActionResult> ProcessPremiumPayment(BuyPremiumRequest request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new BuyPremiumCommand(request), cancellationToken);

        if (!response.IsSuccessfully)
            return BadRequest(response);

        return Ok(response);
    }
}