using MediatR;
using Microsoft.AspNetCore.Mvc;
using YouTube.Application.Common.Requests.Base;
using YouTube.Application.Common.Requests.Playlist;
using YouTube.Application.Features.Playlist.AddVideoToHistory;
using YouTube.Application.Features.Playlist.AddVideoToWatchLater;
using YouTube.Application.Features.Playlist.CreatePlaylist;
using YouTube.Application.Features.Playlist.GetPlaylistById;

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

    /// <summary>
    /// Добавить видео в плейлист истории
    /// </summary>
    /// <param name="request">Запрос</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns></returns>
    [HttpPost("VideoToHistoryPlaylist")]
    public async Task<IActionResult> AddVideoToHistoryPlaylist(AddVideoToPlaylistRequest request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new AddVideoToHistoryPlaylistCommand(request), cancellationToken);

        if (response.IsSuccessfully)
            return Ok(response);

        return BadRequest(response);
    }

    /// <summary>
    /// Добавить видео в плейлист посметреть позже
    /// </summary>
    /// <param name="request">Запрос</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns></returns>
    [HttpPost("VideoToWatchLaterPlaylist")]
    public async Task<IActionResult> AddVideoToWatchLaterPlaylist(AddVideoToPlaylistRequest request,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new AddVideoToWatchLaterPlaylistCommand(request), cancellationToken);

        if (response.IsSuccessfully)
            return Ok(response);

        return BadRequest(response);
    }

    /// <summary>
    /// Получить плейлист по id
    /// </summary>
    /// <param name="id">Id плейлиста</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Плейлист</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPlaylistById([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetPlaylistByIdQuery(new IdRequest { Id = id }), cancellationToken);

        if (response.IsSuccessfully)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }
}