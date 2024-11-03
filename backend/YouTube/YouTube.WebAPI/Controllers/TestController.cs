using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using YouTube.Application.Common.Exceptions;
using YouTube.Application.Common.Requests.Base;
using YouTube.Application.DTOs.Video;
using YouTube.Application.Features.Video.GetVideo;
using YouTube.Application.Interfaces;
using YouTube.Domain.Entities;
using YouTube.Infrastructure.Hubs;
using File = YouTube.Domain.Entities.File;

namespace YouTube.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly IS3Service _service;
    private readonly IMediator _mediator;
    private readonly IDbContext _context;
    private readonly IEmailService _emailService;


    public TestController(IS3Service service,IMediator mediator, IDbContext context,IEmailService emailService,  UserManager<User> userManager)
    {
        _service = service;
        _mediator = mediator;
        _context = context;
        _emailService = emailService;
        _userManager = userManager;
    }

    [HttpGet("GetLink")]
    public async Task<IActionResult> GetLink(string bucketId, string objectName, CancellationToken cancellationToken)
    {
        var link = await _service.GetFileUrlAsync(bucketId, objectName, cancellationToken);

        if (link != String.Empty)
            return Ok(link);

        return BadRequest("Pizda");
    }

    [HttpGet("TetsDate")]
    public async Task<IActionResult> GetDate(CancellationToken cancellationToken)
    {
        var date = await _context.ChatMessages.ToListAsync(cancellationToken);
        foreach (var times in date)
        {
            Console.WriteLine($"times.Timestamp.TimeOfDay: {times.Timestamp.TimeOfDay}");
            Console.WriteLine($"times.Timestamp: {times.Timestamp}");
            Console.WriteLine($"times.Timestamp.ToString(\"HH:mm\"): {times.Timestamp.ToString("HH:mm")}");
            Console.WriteLine($"times.Timestamp.Date: {times.Timestamp.Date}");
        }

        return Ok();
    }
    
    
    [HttpGet("GetVideoLink")]
    public async Task<IActionResult> GetVideoLink(CancellationToken cancellationToken)
    {
        var kink = await _service.GetObjectAsync("avatar", "IMG_4520.MP4", cancellationToken);
        return Ok(kink);
    }
    
    [HttpGet("EmailTest")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(200)]
    [ProducesResponseType(401)]
    public async Task<IActionResult> EmailTest()
    {
        await _emailService.SendEmailAsync("bulatfri18@gmail.com", "Хуй", "ХУЙ");

        return Ok();
    }
    
    [HttpPost("HIHIHIHHI")]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> Hihihihih(CancellationToken cancellationToken)
    {
        var users = await _context.Users.ToListAsync(cancellationToken);

        foreach (var user in users)
        {
            var rolesAsync = await _userManager.GetRolesAsync(user);

            foreach (var role in rolesAsync)
            {
                Console.WriteLine(role);
            }
        }
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

  
    
    [HttpDelete("Delete")]
    public async Task<IActionResult> DeleteAllUser(Guid id ,CancellationToken cancellationToken)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id, cancellationToken) ?? throw new NotFoundException(); 
        _context.Users.Remove(user);

        await _context.SaveChangesAsync(cancellationToken);
        
        return Ok();
    }
    [HttpPost("AddChat")]
    public async Task<IActionResult> AddMessage(CancellationToken cancellationToken)
    {
        // Получаем пользователя
        var user = await _context.Users.Include(x => x.ChatHistory).FirstOrDefaultAsync(
            x => x.Id == Guid.Parse("206e4698-3f1b-42aa-a2b1-15e980da081f"), cancellationToken) ?? throw new NotFoundException();

       
        user.ChatHistory = new ChatHistory();


        var messages = new List<ChatMessage>
        {
            new ChatMessage
            {
                Message = "Даун?",
                Timestamp = DateTime.UtcNow,
                IsRead = false,
                UserId = user.Id,
                User = user,
                ChatHistory = user.ChatHistory
            },
            
            new ChatMessage
            {
                Message = "Добро",
                Timestamp = DateTime.UtcNow.AddMinutes(2), 
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