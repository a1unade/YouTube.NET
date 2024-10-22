using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using YouTube.Application.DTOs.Chat;
using YouTube.Application.DTOs.Video;
using YouTube.Application.Interfaces;

namespace YouTube.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ChatController : ControllerBase
{
    private readonly IChatService _chatService;

    public ChatController(IChatService chatService)
    {
        _chatService = chatService;
    }

    [HttpPost("Test")]
    public async Task<IActionResult> TestBus(Guid userId, string? messageInfo, IFormFile? file)
    {
        if (file is not null)
        {
            byte[] fileBytes;
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                fileBytes = memoryStream.ToArray();
            }

            var fileContent = new FileContent
            {
                ContentType = file.ContentType,
                FileName = file.FileName,
                Lenght = file.Length,
                Bucket = userId.ToString(),
                Bytes = fileBytes
            };

            await _chatService.SendMessageAsync(new MessageInfo
            {
                UserId = userId,
                Message = messageInfo.IsNullOrEmpty() ? null : messageInfo,
                FileContent = fileContent
            });
        }
        else
        {
            await _chatService.SendMessageAsync(new MessageInfo
            {
                UserId = userId,
                Message = messageInfo.IsNullOrEmpty() ? null : messageInfo,
                FileContent = null
            });
        }

        return Ok("Заебись");
    }
}