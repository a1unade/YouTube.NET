using Microsoft.AspNetCore.Mvc;
using YouTube.Application.Common.Enums;
using YouTube.Application.Common.Responses;
using YouTube.Application.DTOs.Auth;
using YouTube.Application.Interfaces;

namespace YouTube.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(IAuthService authService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto registerDto)
    {
        AuthResponse result = await authService.RegisterAsync(registerDto);
        
        if (result.Type == UserResponseTypes.Success)
            return Ok(result);
        
        return BadRequest(result.Message);
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        AuthResponse result = await authService.SignInAsync(loginDto);
        
        if (result.Type == UserResponseTypes.Success)
            return Ok(result);
        
        return BadRequest(result.Message);
    }
    
    [HttpGet("confirm-email")]
    public async Task<AuthResponse> ConfirmEmail(string userId, string token)
    {
        AuthResponse result = await authService.ConfirmEmailAsync(userId, token);
        
        if (result.Type == UserResponseTypes.Success)
            return result;
        
        return result;
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> DeleteAllUsers()
    {
        await authService.DeleteAllUsersAsync();

        return Ok(new { Message = "Пользователи были удалены"});
    }
}