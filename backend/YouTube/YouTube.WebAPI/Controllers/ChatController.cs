using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using YouTube.Application.Common.Requests.Base;
using YouTube.Application.Common.Requests.Chats;
using YouTube.Application.DTOs.Chat;
using YouTube.Application.DTOs.Video;
using YouTube.Application.Features.Chats.GetChatInfoCollectionForCard;
using YouTube.Application.Features.Chats.GetChatMessagesPaginationByDay;
using YouTube.Application.Interfaces;

namespace YouTube.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ChatController : ControllerBase
{
    private readonly IChatService _chatService;
    private readonly IMediator _mediator;

    public ChatController(IChatService chatService, IMediator mediator)
    {
        _chatService = chatService;
        _mediator = mediator;
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
    
    /// <summary>
    /// Запрос на получение чата для карточки
    /// </summary>
    /// <param name="request">Запрос</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о чате</returns>
    [HttpGet("GetHistoryCollectionForCard")]
    public async Task<IActionResult> GetChatInfoCollectionMessage([FromQuery] PaginationRequest request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetChatInfoCollectionQuery(request), cancellationToken);
        if (response.IsSuccessfully)
            return Ok(response);
        
        return BadRequest(response);
    }
    
    /// <summary>
    /// Запрос на получение сообщений по дням
    /// </summary>
    /// <param name="request">Запрос</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Список сообщений по дням</returns>
    [HttpGet("ChatMessagesByDay")]
    public async Task<IActionResult> GetChatMessagesPaginationByDay([FromQuery] PaginationDaysRequest request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetChatMessagesPaginationQuery(request), cancellationToken);
        if (response.IsSuccessfully)
            return Ok(response);
        
        return BadRequest(response);
    }
}