using MediatR;
using Microsoft.AspNetCore.Mvc;
using YouTube.Application.Common.Requests.Base;
using YouTube.Application.Common.Requests.Email;
using YouTube.Application.Common.Requests.User;
using YouTube.Application.Features.Email.CodeCheckForChangePassword;
using YouTube.Application.Features.Email.CodeCheckForConfirmEmail;
using YouTube.Application.Features.Email.ForgotPasswordSendEmail;
using YouTube.Application.Features.UserRequests.ChangePassword;
using YouTube.Application.Features.UserRequests.CheckUserEmail;
using YouTube.Application.Features.UserRequests.ConfirmEmail;
using YouTube.Application.Features.UserRequests.GetById;

namespace YouTube.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    /// <summary>
    /// Запрос на отправку кода для восстановления пароля
    /// </summary>
    /// <param name="request">Почта</param>
    /// <param name="cancellationToken">Токен отмены</param>
    [HttpPost("ForgotPassword")]
    public async Task<IActionResult> ForgotPassword(EmailRequest request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new ForgotPasswordSendEmailCommand(request), cancellationToken);

        if (result.IsSuccessfully)
            return Ok(result);

        return BadRequest(result);
    }
    
    /// <summary>
    /// Запрос на проверку кода для восстановления пароля
    /// </summary>
    /// <param name="request">Код</param>
    /// <param name="cancellationToken">Токен отмены</param>
    [HttpPost("CodeCheckForChangePassword")]
    public async Task<IActionResult> CodeCheckForChangePassword(CodeCheckForChangePasswordRequest request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new CodeCheckForChangePasswordCommand(request), cancellationToken);

        if (result.IsSuccessfully)
            return Ok(result);

        return BadRequest(result);
    }

    /// <summary>
    /// Запрос на отправку сообщения с кодом подтверждения
    /// </summary>
    /// <param name="request">Почта пользователя</param>
    /// <param name="cancellationToken">Токен отмены</param>
    [HttpPost("ConfirmEmail")]
    public async Task<IActionResult> ConfirmEmail(EmailRequest request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new ConfirmEmailCommand(request), cancellationToken);

        if (result.IsSuccessfully)
            return Ok(result);

        return BadRequest(result);
    }
    
    /// <summary>
    /// Проверка кода для подтверждения почты
    /// </summary>
    /// <param name="request">Код и Id пользователя</param>
    /// <param name="cancellationToken">Токен отмены</param>
    [HttpPost("CodeCheckForEmail")]
    public async Task<IActionResult> CodeCheckForEmail(CodeCheckRequest request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new CodeCheckForConfirmEmailCommand(request), cancellationToken);
        if (result.IsSuccessfully)
        {
            return Ok(result);
        }

        return BadRequest(result);
    }
     
    /// <summary>
    /// Проверка почты юзера
    /// </summary>
    /// <param name="request">Почта пользователя</param>
    /// <param name="cancellationToken">Токен отмены</param>
    [HttpPost("CheckUserEmail")]
    public async Task<IActionResult> CheckUserEmail(EmailRequest request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new CheckUserEmailCommand(request), cancellationToken);
        return Ok(result);
    }
    
    /// <summary>
    /// Смена пароля
    /// </summary>
    /// <param name="request">Почта пользователя</param>
    /// <param name="cancellationToken">Токен отмены</param>
    [HttpPost("ChangePassword")]
    public async Task<IActionResult> ChangePassword(ChangePasswordRequest request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new ChangePasswordCommand(request), cancellationToken);
        
        if (result.IsSuccessfully)
            return Ok(result);
        
        return BadRequest(result);
    }

    /// <summary>
    /// Получить информацию о пользователе
    /// </summary>
    /// <param name="idRequest">Id пользователя</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о пользователе</returns>
    [HttpGet("GetUserById")]
    public async Task<IActionResult> GetUserById([FromQuery] IdRequest idRequest, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetUserByIdQuery(idRequest), cancellationToken);

        if (response.IsSuccessfully)
            return Ok(response);

        return Forbid();
    }
}