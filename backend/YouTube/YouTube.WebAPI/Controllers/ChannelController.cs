using Microsoft.AspNetCore.Mvc;
using YouTube.Application.Interfaces;

namespace YouTube.WebAPI.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ChannelController : ControllerBase
{
    private readonly IVideoService _videoService;

    public ChannelController(IVideoService videoService)
    {
        _videoService = videoService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllVideos()
    {
        var videos = await _videoService.GetAllVideo();
        return Ok(videos);
    }

    [HttpPost("upload")]
    public async Task<IActionResult> UploadVideo(IFormFile video)
    {
        // в байтах
        Console.WriteLine(video.Length);
        // в мегабайтах 
        Console.WriteLine(video.Length / (1024 * 1024));
        if (video == null || video.Length == 0)
        {
            return BadRequest("Файл не загружен");
        }

        if (video.Length / (1024 * 1024) < 20)
        {
            return BadRequest("Много весит");
        }

        await _videoService.AddVideo(video);

        return Ok("Видео успешно загружено");
    }
}