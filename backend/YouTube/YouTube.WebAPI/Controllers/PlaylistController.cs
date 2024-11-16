using MediatR;
using Microsoft.AspNetCore.Mvc;
using YouTube.Application.Common.Requests.Playlist;
using YouTube.Application.Features.Playlist.CreatePlaylist;

namespace YouTube.WebAPI.Controllers;
[ApiController]
[Route("[action]")]
public class PlaylistController : ControllerBase
{
    private readonly IMediator _mediator;

    public PlaylistController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Создать плейлист
    /// </summary>
    /// <param name="request">request</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns></returns>
    [HttpPost("CreatePlaylist")]
    public async Task<IActionResult> CreatePlaylist(CreatePlaylistRequest request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new CreatePlaylistCommand(request), cancellationToken);

        if (response.IsSuccessfully)
            return Ok(response);

        return BadRequest(response);
    }
}