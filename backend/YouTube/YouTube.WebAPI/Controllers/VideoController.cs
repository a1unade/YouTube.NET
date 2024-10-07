using MediatR;
using Microsoft.AspNetCore.Mvc;
using YouTube.Application.Common.Requests.Video;
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

    [HttpPost("UploadVideo")]
    public async Task<IActionResult> UploadVideo(UploadVideoRequest request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new UploadVideoCommand(request), cancellationToken);

        if (response.IsSuccessfully)
            return Ok(response);
        return BadRequest(request);
    }
}