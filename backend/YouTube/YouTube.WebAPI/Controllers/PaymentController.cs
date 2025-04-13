using Grpc.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YouTube.Application.Common.Requests.Base;
using YouTube.Application.Common.Requests.Payment;
using YouTube.Application.Features.Payment.BuyPremium;
using YouTube.Application.Features.Payment.CreateWallet;
using YouTube.Application.Features.Payment.GetBalance;
using YouTube.Application.Features.Payment.UpdateBalance;
using YouTube.Application.Interfaces;
using YouTube.Domain.Common;
using YouTube.Domain.Entities;
using YouTube.Proto;
using CreateWalletRequest = YouTube.Application.Common.Requests.Payment.CreateWalletRequest;

namespace YouTube.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class PaymentController : ControllerBase
{
    private readonly IDbContext _context;
    private readonly PaymentService.PaymentServiceClient _paymentClient;
    private readonly IMediator _mediator;


    public PaymentController(IDbContext context, PaymentService.PaymentServiceClient paymentClient, IMediator mediator)
    {
        _context = context;
        _paymentClient = paymentClient;
        _mediator = mediator;
    }


    [HttpPost("[action]")]
    public async Task<IActionResult> CreateWallet([FromBody] CreateWalletRequest request,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new CreateWalletCommand(request), cancellationToken);

        if (!response.IsSuccessfully)
            return BadRequest(response);

        return Ok(response);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> UpdateBalance(UpdateBalanceRequest request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new UpdateBalanceCommand(request), cancellationToken);

        if (!response.IsSuccessfully)
            return BadRequest(response);

        return Ok(response);
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> GetBalance([FromQuery] IdRequest request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetBalanceQuery(request), cancellationToken);

        if (!response.IsSuccessfully)
            return BadRequest(response);
        
        return Ok(response);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> ProcessPremiumPayment(BuyPremiumRequest request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new BuyPremiumCommand(request), cancellationToken);

        if (!response.IsSuccessfully)
            return BadRequest(response);

        return Ok(response);
    }
}