using MediatR;
using Microsoft.AspNetCore.Mvc;
using YouTube.Application.Common.Requests.Base;
using YouTube.Application.Common.Requests.Channel;
using YouTube.Application.Features.Channel.GetChannel;
using YouTube.Application.Features.Channel.GetChannelLinks;
using YouTube.Application.Features.Channel.SubscribeToChannel;

namespace YouTube.WebAPI.Controllers;
[ApiController]
[Route("[controller]")]
public class ChannelController : ControllerBase
{
    private readonly IMediator _mediator;

    public ChannelController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Получить канал по Id 
    /// </summary>
    /// <param name="request">Id канала</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Канал</returns>
    [HttpGet("GetChannel")]
    public async Task<IActionResult> GetChannelById([FromQuery] IdRequest request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetChannelQuery(request), cancellationToken);
        if (response.IsSuccessfully)
            return Ok(response);

        return BadRequest(response);
    }

    /// <summary>
    /// Получить подробную информации о канале
    /// </summary>
    /// <param name="request">Id канала</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Подробная информация о канале вместе со ссылками</returns>
    [HttpGet("GetChannelLinks")]
    public async Task<IActionResult> GetChannelWithLinks([FromQuery] IdRequest request,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetChannelLinksQuery(request), cancellationToken);

        if (response.IsSuccessfully)
            return Ok(response);

        return NotFound(response);
    }

    /// <summary>
    /// Подписаться на канал
    /// </summary>
    /// <param name="request">ID каналов</param>
    /// <param name="cancellation">Токен отмены</param>
    /// <returns></returns>
    [HttpPost("SubscribeToChannel")]
    public async Task<IActionResult> SubscribeToChannel(SubscribeToChannelRequest request,
        CancellationToken cancellation)
    {
        var response = await _mediator.Send(new SubscribeToChannelCommand(request), cancellation);

        if (response.IsSuccessfully)
            return Ok(response);

        return BadRequest(response);
    }
}