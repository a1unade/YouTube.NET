using MediatR;
using Microsoft.AspNetCore.Mvc;
using YouTube.Application.Common.Requests.Base;
using YouTube.Application.Features.Channel.GetChannel;
using YouTube.Application.Features.Channel.GetChannelLinks;

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
    /// <returns>подробнуа информация о канале вместе с ссылками</returns>
    [HttpGet("GetChannelLinks")]
    public async Task<IActionResult> GetChannelWithLinks([FromQuery] IdRequest request,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetChannelLinksQuery(request), cancellationToken);

        if (response.IsSuccessfully)
            return Ok(response);

        return NotFound(response);
    }
}