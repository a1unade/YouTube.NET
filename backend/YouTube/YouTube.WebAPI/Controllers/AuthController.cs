using MediatR;
using Microsoft.AspNetCore.Mvc;
using YouTube.Application.Common.Requests.Auth;
using YouTube.Application.Features.Auth.Authorization;
using YouTube.Application.Features.Auth.Login;
using YouTube.Application.Features.Auth.Logout;

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


    [HttpPost("[action]")]
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

    [HttpPost("[action]")]
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

    [HttpPost("[action]")]
    public async Task<IActionResult> Logout(LogoutRequest request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new LogoutCommand(request), cancellationToken);

        if (result.IsSuccessfully)
            Response.Cookies.Delete("authCookie");

        return Ok();
    }
}