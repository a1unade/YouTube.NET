using Microsoft.AspNetCore.Mvc;
using YouTube.Application.DTOs.Comment;
using YouTube.Application.Interfaces;

namespace YouTube.WebAPI.Controllers;
[ApiController]
public class VideoController : ControllerBase
{
    private readonly IVideoService _service;

    public VideoController(IVideoService service)
    {
        _service = service;
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> GetVideoComment(int videoId, CancellationToken cancellationToken)
    {
        var result = await _service.GetVideoCommentList(videoId, cancellationToken);

        if (!result.IsSuccessfully)
            return BadRequest(result);

        return Ok(result);
    }
    
    [HttpPost("[action]")]
    public async Task<IActionResult> AddComment(AddCommentDto request, CancellationToken cancellationToken)
    {
        var result = await _service.AddComment(request, cancellationToken);

        if (!result.IsSuccessfully)
            return BadRequest(result);

        return Ok(result);
    }
}