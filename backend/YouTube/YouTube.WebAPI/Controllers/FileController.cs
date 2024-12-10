using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YouTube.Application.Common.Exceptions;
using YouTube.Application.Common.Requests.Base;
using YouTube.Application.Common.Requests.Chats;
using YouTube.Application.Features.Files.GetFileStream;
using YouTube.Application.Features.Files.UploadMessageFile;
using YouTube.Application.Interfaces;

namespace YouTube.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class FileController : ControllerBase
{
    private readonly IS3Service _s3Service;
    private readonly IDbContext _context;
    private readonly IMediator _mediator;

    public FileController(IS3Service s3Service, IDbContext context, IMediator mediator)
    {
        _s3Service = s3Service;
        _context = context;
        _mediator = mediator;
    }

    /// <summary>
    /// Загрузить файл из чата
    /// </summary>
    /// <param name="request">Запрос</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns></returns>
    [HttpPost("[action]")]
    public async Task<IActionResult> UploadFile(UploadMessageFileRequest request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new UploadMessageFileCommand(request), cancellationToken);

        if (response.IsSuccessfully)
            return Ok(response);

        return BadRequest(response);
    }
    
    /// <summary>
    /// Получить stream файла
    /// </summary>
    /// <param name="id">Id Видео</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Stream файла</returns>
    [HttpGet("GetFileStream/{id}")]
    public async Task<IActionResult> GetFileStream([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetFileStreamQuery(new IdRequest { Id = id }), cancellationToken);
    
        if (response.IsSuccessfully && response.ContentType != null!)
        {
            HttpContext.Response.Headers.Add("Content-Disposition", $"attachment; filename={response.FileName}");
            return new FileStreamResult(response.Stream, response.ContentType) { FileDownloadName = response.FileName };
        }

        return BadRequest(response);
    }

    /// <summary>
    /// Получить stream файла
    /// </summary>
    /// <param name="id">Id Видео</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Stream файла</returns>
    [HttpGet("GetVideoStream/{id}")]
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