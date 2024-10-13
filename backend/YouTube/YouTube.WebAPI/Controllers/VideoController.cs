using MediatR;
using Microsoft.AspNetCore.Mvc;
using YouTube.Application.Common.Requests.Base;
using YouTube.Application.Common.Requests.Video;
using YouTube.Application.Features.Video.GetVideo;
using YouTube.Application.Features.Video.UploadVideo;

namespace YouTube.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class VideoController : ControllerBase
{
    private readonly IMediator _mediator;

    public VideoController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Загрузить видео
    /// </summary>
    /// <param name="request">Запрос</param>
    /// <param name="cancellationToken">cancellationToken</param>
    [HttpPost("UploadVideo")]
    public async Task<IActionResult> UploadVideo(UploadVideoRequest request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new UploadVideoCommand(request), cancellationToken);

        if (response.IsSuccessfully)
            return Ok(response);
        return BadRequest(request);
    }

    /// <summary>
    /// Получить видео по Id
    /// </summary>
    /// <param name="request">Id видео</param>
    /// <param name="cancellationToken">cancellationToken</param>
    /// <returns></returns>
    [HttpGet("GetVideoById")]
    public async Task<IActionResult> GetVideoById([FromQuery ] IdRequest request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetVideoQuery(request), cancellationToken);

        if (response.IsSuccessfully)
            return Ok(response);

        return BadRequest(response);
    }
}