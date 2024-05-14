using Microsoft.AspNetCore.Mvc;
using YouTube.Application.DTOs.Channel;
using YouTube.Application.Interfaces;

namespace YouTube.WebAPI.Controllers;
[ApiController]
public class ChannelController : ControllerBase
{
    private readonly IChannelService _channelService;
    private readonly IVideoService _videoService;

    public ChannelController(IChannelService channelService,
        IVideoService videoService)
    {
        _channelService = channelService;
        _videoService = videoService;
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> GetChannelByName(string name, CancellationToken cancellationToken)
    {
        var result = await _channelService.GetByName(name, cancellationToken);

        if (!result.IsSuccessfully)
            return BadRequest(result);

        return Ok(result);
    }
    
    [HttpGet("[action]")]
    public async Task<IActionResult> GetAllChannelVideo([FromQuery] int id, CancellationToken cancellationToken)
    {
        var result = await _channelService.GetChannelVideo(id, cancellationToken);

        if (!result.IsSuccessfully)
            return BadRequest(result);

        return Ok(result);
    }
    
    [HttpPost("[action]")]
    public async Task<IActionResult> CreateChannel(CreateChannelDto request, CancellationToken cancellationToken)
    {
        var result = await _channelService.CreateChannel(request, cancellationToken);

        if (!result.IsSuccessfully)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> GetRandomVideo(CancellationToken cancellationToken)
    {
        var t = await _videoService.GetRandomVideo(cancellationToken);

        if(t.IsSuccessfully)
            return Ok(t);

        return BadRequest(t);
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> GetVideoById(int id, CancellationToken cancellationToken)
    {
        var res = await _videoService.GetVideoById(id, cancellationToken);

        if (res.IsSuccessfully)
            return Ok(res);

        return BadRequest(res);
    }
}