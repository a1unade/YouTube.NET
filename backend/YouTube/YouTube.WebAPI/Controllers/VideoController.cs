using MediatR;
using Microsoft.AspNetCore.Mvc;
using YouTube.Application.Common.Requests.Base;
using YouTube.Application.Common.Requests.Video;
using YouTube.Application.Features.Video.GetVideo;
using YouTube.Application.Features.Video.GetVideoPagination;
using YouTube.Application.Features.Video.IncrementView;
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
    ///  Добавить просмотр на видео
    /// </summary>
    /// <param name="request">Запрос</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns></returns>
    [HttpPost("IncrementView")]
    public async Task<IActionResult> IncrementView(IncrementViewRequest request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new IncrementViewCommand(request), cancellationToken);
        
        if(response.IsSuccessfully)
            return Ok(response);
        
        return BadRequest(response);
    }

    /// <summary>
    /// Загрузить видео
    /// </summary>
    /// <param name="request">Запрос</param>
    /// <param name="cancellationToken">Токен отмены</param>
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
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns></returns>
    [HttpGet("GetVideoById")]
    public async Task<IActionResult> GetVideoById([FromQuery] IdRequest request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetVideoQuery(request), cancellationToken);

        if (response.IsSuccessfully)
            return Ok(response);

        return BadRequest(response);
    }

    /// <summary>
    /// Получить список видео
    /// </summary>
    /// <param name="request">Страница, размер, категория, сортировка</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns></returns>
    [HttpGet("PaginationVideo")]
    public async Task<IActionResult> PaginationVideo([FromQuery] VideoPaginationRequest request,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetVideoPaginationQuery(request), cancellationToken);
        if (response.IsSuccessfully)
            return Ok(response);

        return BadRequest(response);
    }
}