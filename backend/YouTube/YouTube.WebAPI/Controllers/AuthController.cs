using MediatR;
using Microsoft.AspNetCore.Mvc;
using YouTube.Application.Common.Requests.Auth;
using YouTube.Application.Features.Auth.Authorization;
using YouTube.Application.Features.Auth.Login;
using YouTube.Application.Features.Auth.Logout;
using YouTube.Application.Features.Auth.RefreshToken;

namespace YouTube.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    /// <summary>
    /// Авторизация
    /// </summary>
    /// <param name="request">Запрос</param>
    /// <param name="cancellationToken">Токен отмены</param>
    [HttpPost("Auth")]
    public async Task<IActionResult> AuthUser(AuthRequest request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new AuthCommand(request), cancellationToken);

        if (result.IsSuccessfully)
        {
            Response.Cookies.Append("authCookie", result.Token, new CookieOptions
            {
                Path = "/",
                Expires = DateTimeOffset.Now.AddHours(2),
            });
            return Ok(result);
        }

        return NotFound(result);
    }

    /// <summary>
    /// Логин
    /// </summary>
    /// <param name="request">Запрос</param>
    /// <param name="cancellationToken">Токен отмены</param>
    [HttpPost("Login")]
    public async Task<IActionResult> LoginUser(LoginRequest request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new LoginCommand(request), cancellationToken);

        if (result.IsSuccessfully)
        {
            Response.Cookies.Append("authCookie", result.Token, new CookieOptions
            {
                Path = "/",
                Expires = DateTimeOffset.Now.AddHours(2),
            });
            return Ok(result);
        }

        return NotFound(result);
    }

    [HttpPost("RefreshToken")]
    public async Task<IActionResult> RefreshToken(RefreshTokenRequest request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new RefreshTokenCommand(request), cancellationToken);
        
        if(response.IsSuccessfully)
            return Ok(response);
        
        return BadRequest(response);
    }
    
    /// <summary>
    /// Выход из аккаунта
    /// </summary>
    /// <param name="request">Запрос</param>
    /// <param name="cancellationToken">Токен отмены</param>
    [HttpPost("Logout")]
    public async Task<IActionResult> Logout(LogoutRequest request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new LogoutCommand(request), cancellationToken);

        if (result.IsSuccessfully)
            Response.Cookies.Delete("authCookie");

        return Ok();
    }
}