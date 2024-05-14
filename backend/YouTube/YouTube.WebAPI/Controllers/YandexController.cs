using Microsoft.AspNetCore.Mvc;
using YouTube.Application.DTOs.Video;
using YouTube.Application.Interfaces;

namespace YouTube.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class YandexController : ControllerBase
{
    private readonly IYandexService _yandexService;
    private readonly IConfiguration _configuration;

    public YandexController(IYandexService yandexService, IConfiguration configuration)
    {
        _yandexService = yandexService;
        _configuration = configuration;
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> GetAllFilesOnDisk(CancellationToken cancellationToken)
    {
        var yandex = await _yandexService.GetAllFilesOnDisk(cancellationToken);

        return Ok(yandex);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> UploadFileToDisk(AddVideoDto request, CancellationToken cancellationToken)
    {
        var result = await _yandexService.UploadFileToDisk(request.Video, request.ImgUrl, request.Name, request.Description, request.UserId, cancellationToken);

        if (!result.IsSuccessfully)
            return BadRequest(result);

        return Ok(result);
    }
    
    [HttpGet("[action]")]
    public async Task<IActionResult> GetFileFromPath(string path, CancellationToken cancellationToken)
    {
        var result = await _yandexService.GetFile(path, cancellationToken);
        
        if (!result.IsSuccessfully)
            return BadRequest(result);
    
        return Ok(result);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> CreateFolder(string path, CancellationToken cancellationToken)
    {
        var result = await _yandexService.CreateFolder(path, cancellationToken);

        if (!result.IsSuccessfully)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpDelete("[action]")]
    public async Task<IActionResult> DeleteFile(string path, CancellationToken cancellationToken)
    {
        var result = await _yandexService.DeleteFile(path, cancellationToken);

        if (!result.IsSuccessfully)
            return BadRequest(result);

        return Ok(result);
    }

    

    [HttpGet("[action]")]
    public async Task<IActionResult> PublishFile(string path, CancellationToken cancellationToken)
    {
        var result = await _yandexService.PublishFile(path, cancellationToken);

        if (!result.IsSuccessfully)
            return BadRequest(result);

        return Ok(result);
    }
    
}