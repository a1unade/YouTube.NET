using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YouTube.Application.Common.Requests.Base;
using YouTube.Application.DTOs.Video;
using YouTube.Application.Features.Video.GetVideo;
using YouTube.Application.Interfaces;
using YouTube.Domain.Entities;
using File = YouTube.Domain.Entities.File;

namespace YouTube.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    private readonly IS3Service _service;
    private readonly IMediator _mediator;
    private readonly IDbContext _context;
    private readonly IEmailService _emailService;

    public TestController(IS3Service service,IMediator mediator, IDbContext context,IEmailService emailService)
    {
        _service = service;
        _mediator = mediator;
        _context = context;
        _emailService = emailService;
    }

    [HttpGet("GetLink")]
    public async Task<IActionResult> GetLink(string bucketId, string objectName, CancellationToken cancellationToken)
    {
        var link = await _service.GetFileUrlAsync(bucketId, objectName, cancellationToken);

        if (link != String.Empty)
            return Ok(link);

        return BadRequest("Pizda");
    }
    
    
    [HttpGet("GetVideoLink")]
    public async Task<IActionResult> GetVideoLink(CancellationToken cancellationToken)
    {
        var kink = await _service.GetObjectAsync("avatar", "IMG_4520.MP4", cancellationToken);
        return Ok(kink);
    }
    
    [HttpGet("EmailTest")]
    public async Task<IActionResult> EmailTest()
    {
        await _emailService.SendEmailAsync("bulatfri18@gmail.com", "Хуй", "ХУЙ");

        return Ok();
    }

    [HttpGet("GetVideoById")]
    public async Task<IActionResult> GetVideo(IdRequest request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetVideoQuery(request), cancellationToken);
        if (response.IsSuccessfully)
            return Ok(response);

        return BadRequest(response);    
    }

    /// <summary>
    /// Получить канал по Id 
    /// </summary>
    /// <param name="files"></param>
    /// <param name="cancellationToken">cancellationToken</param>
    /// <returns>Канал</returns>
    [HttpPost("UploadFilesForChannel")]
    public async Task<IActionResult> UploadFilesForChannel(List<IFormFile> files, CancellationToken cancellationToken)
    {
        var channel =
            await _context.Channels.FirstOrDefaultAsync(x => x.Id == Guid.Parse("bba9b7e4-33e6-447e-b33a-cc5c60d77365"),
                cancellationToken);
        foreach (var file in files)
        {
            var fileToDb = new File
            {
                Size = file.Length,
                ContentType = file.ContentType,
                Path = "fafwf",
                FileName = file.FileName,
                BucketName = channel!.Id.ToString()
            };

            await _context.Files.AddAsync(fileToDb, cancellationToken);
            if (fileToDb.FileName == "GIN.jpg")
                channel.BannerImg = fileToDb;

            else
                channel.MainImgFile = fileToDb;
            
            await _context.SaveChangesAsync(cancellationToken);

            await _service.UploadAsync(new FileContent
            {
                Content = file.OpenReadStream(),
                FileName = file.FileName,
                ContentType = file.ContentType,
                Lenght = file.Length,
                Bucket = channel.Id.ToString()
            }, cancellationToken);

        }

        return Ok();
    }

  
    
    [HttpDelete("DeleteAll")]
    public async Task<IActionResult> DeleteAllUser(CancellationToken cancellationToken)
    {
        var user = await _context.Users.ToListAsync(cancellationToken); 
        _context.Users.RemoveRange(user);

        await _context.SaveChangesAsync(cancellationToken);
        
        return Ok();
    }
    [HttpPost("AddMessage")]
    public async Task<IActionResult> AddMessage(CancellationToken cancellationToken)
    {
        // Получаем пользователя
        var user = await _context.Users.Include(x => x.ChatHistory).FirstOrDefaultAsync(
            x => x.Id == Guid.Parse("d2f2139c-ae5e-40b6-98d1-291d14cc8862"), cancellationToken);

        user.ChatHistory = new ChatHistory();

        // Добавляем несколько сообщений в чат
        var messages = new List<ChatMessage>
        {
            new ChatMessage
            {
                Message = "Привет?",
                Timestamp = DateTime.UtcNow, // Установка времени в UTC
                IsRead = false,
                UserId = user.Id,
                User = user,
                ChatHistory = user.ChatHistory
            },
            new ChatMessage
            {
                Message = "спасибо!",
                Timestamp = DateTime.UtcNow.AddMinutes(1), // Смещение времени для примера в UTC
                IsRead = false,
                UserId = user.Id,
                User = user,
                ChatHistory = user.ChatHistory
            },
            new ChatMessage
            {
                Message = "на выходных?",
                Timestamp = DateTime.UtcNow.AddMinutes(2), // Смещение времени для примера в UTC
                IsRead = false,
                UserId = user.Id,
                User = user,
                ChatHistory = user.ChatHistory
            }
        };

        _context.ChatMessages.AddRange(messages);
        await _context.SaveChangesAsync(cancellationToken);
    
        return Ok();
    }


}