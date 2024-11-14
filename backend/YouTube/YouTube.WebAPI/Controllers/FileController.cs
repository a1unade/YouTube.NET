using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YouTube.Application.Common.Exceptions;
using YouTube.Application.Interfaces;

namespace YouTube.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class FileController : ControllerBase
{
    private readonly IS3Service _s3Service;
    private readonly IDbContext _context;

    public FileController(IS3Service s3Service, IDbContext context)
    {
        _s3Service = s3Service;
        _context = context;
    }

    /// <summary>
    /// Получить stream файла
    /// </summary>
    /// <param name="id">Id Видео</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Stream файла</returns>
    [HttpGet("GetStream/{id}")]
    public async Task<IActionResult> GetStreamFromVideoAsync([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var video = await _context.Videos
                        .Include(x => x.VideoUrl)
                        .FirstOrDefaultAsync(x => x.Id == id, cancellationToken)
                    ?? throw new NotFoundException();

        var videoFile = video.VideoUrl;
        var stream = await _s3Service.GetFileStreamAsync(videoFile.BucketName, videoFile.FileName, cancellationToken);

        return new FileStreamResult(stream, videoFile.ContentType!)
        {
            FileDownloadName = videoFile.FileName
        };
    }
}