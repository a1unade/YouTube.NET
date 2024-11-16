using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Minio;
using YouTube.Application.Common.Exceptions;
using YouTube.Application.Common.Requests.Base;
using YouTube.Application.Common.Requests.Chats;
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
    private readonly UserManager<User> _userManager;
    private readonly IS3Service _service;
    private readonly IMediator _mediator;
    private readonly IDbContext _context;
    private readonly IEmailService _emailService;
    private readonly IS3Service _s3Service;
    private readonly IBus _bus;

    public TestController(IS3Service service,IMediator mediator, IDbContext context,IEmailService emailService,  UserManager<User> userManager, IMinioClient minioClient, IS3Service s3Service, IBus bus)
    {
        _service = service;
        _mediator = mediator;
        _context = context;
        _emailService = emailService;
        _userManager = userManager;
        _s3Service = s3Service;
        _bus = bus;
    }

    [HttpGet("GetBusRabbit")]
    public async Task<IActionResult> TestRabbit(Guid id,string hui, CancellationToken cancellationToken)
    {
        var user = await _context.Users
                       .Include(x => x.ChatHistory)
                       .FirstOrDefaultAsync(
                       x => x.Id == id, cancellationToken)
                   ?? throw new NotFoundException();

        var messages = await _context.ChatMessages.Where(x => x.UserId == user.Id).ToListAsync(cancellationToken);
        return Ok(new
        {
            user,
            messages
        });
    }
    
    [HttpPost("TestBus")]
    public async Task<IActionResult> TestRab(Guid id, string hui, CancellationToken cancellationToken)
    {
        var user = await _context.Users
                       .Include(x => x.ChatHistory)
                       .FirstOrDefaultAsync(
                           x => x.Id == id, cancellationToken)
                   ?? throw new NotFoundException();

        if (user.ChatHistory == null!)
        {
            user.ChatHistory = new ChatHistory();
            await _context.ChatHistories.AddAsync(user.ChatHistory, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
        
        await _bus.Publish(new SendMessageRequest
        {
            UserId = user.Id,
            ChatId = user.ChatHistory.Id,
            Message = hui
        }, cancellationToken: cancellationToken);

        return Ok("Message published");
    }

    [HttpGet("GetAllMessage")]
    public async Task<IActionResult> GetAllMessage(Guid id, CancellationToken cancellationToken)
    {
        var user = await _context.ChatMessages
                .Where(x => x.UserId == id)
                .ToListAsync(cancellationToken)
            ?? throw new NotFoundException();

        return Ok(new
        {
            user
        });

    }
    
    [HttpGet("GetLink")]
    public async Task<IActionResult> GetLink(string bucketId, string objectName, CancellationToken cancellationToken)
    {
        var link = await _service.GetFileUrlAsync(bucketId, objectName, cancellationToken);

        if (link != String.Empty)
            return Ok(link);

        return BadRequest("Pizda");
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
    
    /// <summary>
    /// Нагенерировать сообщения задним числом для пагинации
    /// </summary>
    /// <param name="chatId">чат ид</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns></returns>
    /// <exception cref="NotFoundException"></exception>
    [HttpPost("AddMessagesForChat")]
    public async Task<IActionResult> AddMessage(Guid chatId, CancellationToken cancellationToken)
    {
        var chat = await _context.ChatHistories
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.Id == chatId, cancellationToken) ?? throw new NotFoundException();
        
        var admin = await _context.Users.FirstOrDefaultAsync(
            x => x.DisplayName == "Main Admin", cancellationToken) ?? throw new NotFoundException();
        
        var messages = new List<ChatMessage>
        {
            new ChatMessage
            {
                Message = "Привет",
                Time = TimeOnly.FromDateTime(DateTime.Now).AddMinutes(-9), 
                Date = DateOnly.FromDateTime(DateTime.Now).AddDays(-4), 
                IsRead = false,
                User = chat.User,
                ChatHistory = chat
            },
            
            new ChatMessage
            {
                Message = "Здравствуйте",
                Time = TimeOnly.FromDateTime(DateTime.Now).AddMinutes(-7), 
                Date = DateOnly.FromDateTime(DateTime.Now).AddDays(-4), 
                IsRead = false,
                User = admin,
                ChatHistory = chat
            },
            
            new ChatMessage
            {
                Message = "Что хотел?",
                Time = TimeOnly.FromDateTime(DateTime.Now).AddMinutes(-5), 
                Date = DateOnly.FromDateTime(DateTime.Now).AddDays(-4), 
                IsRead = false,
                User = chat.User,
                ChatHistory = chat
            },
            
            new ChatMessage
            {
                Message = "Не понял прикола",
                Time = TimeOnly.FromDateTime(DateTime.Now).AddMinutes(-2), 
                Date = DateOnly.FromDateTime(DateTime.Now).AddDays(-4), 
                IsRead = false,
                User = admin,
                ChatHistory = chat
            },
            
            new ChatMessage
            {
                Message = "Алло",
                Time = TimeOnly.FromDateTime(DateTime.Now).AddMinutes(-9), 
                Date = DateOnly.FromDateTime(DateTime.Now).AddDays(-3), 
                IsRead = false,
                User = chat.User,
                ChatHistory = chat
            },
            
            new ChatMessage
            {
                Message = "Кто там?",
                Time = TimeOnly.FromDateTime(DateTime.Now).AddMinutes(-7), 
                Date = DateOnly.FromDateTime(DateTime.Now).AddDays(-3), 
                IsRead = false,
                User = admin,
                ChatHistory = chat
            },
            
            new ChatMessage
            {
                Message = "Не важно",
                Time = TimeOnly.FromDateTime(DateTime.Now).AddMinutes(-5), 
                Date = DateOnly.FromDateTime(DateTime.Now).AddDays(-3), 
                IsRead = false,
                User = chat.User,
                ChatHistory = chat
            },
            
            new ChatMessage
            {
                Message = "Информацию принял",
                Time = TimeOnly.FromDateTime(DateTime.Now).AddMinutes(-2), 
                Date = DateOnly.FromDateTime(DateTime.Now).AddDays(-3), 
                IsRead = false,
                User = admin,
                ChatHistory = chat
            },
            
            
            new ChatMessage
            {
                Message = "Здравствуйте",
                Time = TimeOnly.FromDateTime(DateTime.Now).AddMinutes(-10), 
                Date = DateOnly.FromDateTime(DateTime.Now).AddDays(-1), 
                IsRead = false,
                User = chat.User,
                ChatHistory = chat
            },
            
            new ChatMessage
            {
                Message = "Добрый день",
                Time = TimeOnly.FromDateTime(DateTime.Now).AddMinutes(-9), 
                Date = DateOnly.FromDateTime(DateTime.Now).AddDays(-1), 
                IsRead = false,
                User = admin,
                ChatHistory = chat
            },
            
            new ChatMessage
            {
                Message = "Сколько будет 3 * 3",
                Time = TimeOnly.FromDateTime(DateTime.Now).AddMinutes(-8), 
                Date = DateOnly.FromDateTime(DateTime.Now).AddDays(-1), 
                IsRead = false,
                User = chat.User,
                ChatHistory = chat
            },
            
            new ChatMessage
            {
                Message = "Ну 9, а что?",
                Time = TimeOnly.FromDateTime(DateTime.Now).AddMinutes(-7), 
                Date = DateOnly.FromDateTime(DateTime.Now).AddDays(-1), 
                IsRead = false,
                User = admin,
                ChatHistory = chat
            },
            
            new ChatMessage
            {
                Message = "По приколу написал",
                Time = TimeOnly.FromDateTime(DateTime.Now).AddMinutes(-6), 
                Date = DateOnly.FromDateTime(DateTime.Now).AddDays(-1), 
                IsRead = false,
                User = chat.User,
                ChatHistory = chat
            },
            
            new ChatMessage
            {
                Message = "Пон",
                Time = TimeOnly.FromDateTime(DateTime.Now).AddMinutes(-5), 
                Date = DateOnly.FromDateTime(DateTime.Now).AddDays(-1), 
                IsRead = false,
                User = admin,
                ChatHistory = chat
            }
        };

        _context.ChatMessages.AddRange(messages);
        await _context.SaveChangesAsync(cancellationToken);
        
        return Ok();
    }
}