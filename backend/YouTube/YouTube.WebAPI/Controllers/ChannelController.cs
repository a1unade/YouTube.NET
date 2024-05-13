using Microsoft.AspNetCore.Mvc;
using YouTube.Application.DTOs.Channel;
using YouTube.Application.Interfaces;

namespace YouTube.WebAPI.Controllers;
[ApiController]
public class ChannelController : ControllerBase
{
    private readonly IChannelService _channelService;

    public ChannelController(IChannelService channelService)
    {
        _channelService = channelService;
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
    public async Task<IActionResult> GetAllChannelVideo(int id, CancellationToken cancellationToken)
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
}