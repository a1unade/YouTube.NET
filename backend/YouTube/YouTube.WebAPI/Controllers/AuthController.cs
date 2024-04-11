using Microsoft.AspNetCore.Mvc;
using YouTube.Application.DTOs;
using YouTube.Application.Interfaces;

namespace YouTube.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(IAuthService AuthService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto registerDto)
    {
        var result = await AuthService.RegisterAsync(registerDto);
        
        if (result.Succeeded)
        {
            return Ok(new { Message = "Регистрация успешна" });
        }
        
        return BadRequest(result.Errors);
    }
}