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
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var result = await _mediator.Send(new AuthCommand(request), cancellationToken);

            if (result.IsSuccessfully)
            {
                Response.Cookies.Append("authCookie", result.Token, new CookieOptions
                {
                    Path = "/",
                    Expires = DateTimeOffset.Now.AddHours(2),
                    HttpOnly = true
                });
                return Ok(result);
            }

            return NotFound(result);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> LoginUser(LoginRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _mediator.Send(new LoginCommand(request), cancellationToken);
            
            if (result.IsSuccessfully)
            {
                Response.Cookies.Append("authCookie", result.Token, new CookieOptions
                {
                    Path = "/",
                    Expires = DateTimeOffset.Now.AddHours(2),
                    HttpOnly = true
                });
                return Ok(result);
            }
            return NotFound(result);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
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