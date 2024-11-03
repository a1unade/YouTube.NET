using MediatR;
using Microsoft.AspNetCore.Mvc;
using YouTube.Application.Common.Requests.Base;
using YouTube.Application.Common.Requests.Chats;
using YouTube.Application.Features.Chats.GetChatInfoCollectionForCard;
using YouTube.Application.Features.Chats.GetChatMessagesPaginationByDay;

namespace YouTube.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ChatController : ControllerBase
{
    private readonly IMediator _mediator;

    public ChatController(IMediator mediator)
    {
        _mediator = mediator;
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