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
        UserResponse result = await authService.RegisterAsync(registerDto);
        
        if (result.Type == UserResponseTypes.Success)
            return Ok(result);
        
        return BadRequest(result.Message);
    }
    
    [HttpGet("confirm-email")]
    public async Task<UserResponse> ConfirmEmail(string userId, string token)
    {
        UserResponse result = await authService.ConfirmEmailAsync(userId, token);
        
        if (result.Type == UserResponseTypes.Success)
            return result;
        
        return result;
    }

    [HttpPost("delete")]
    public async Task<IActionResult> DeleteAllUsers()
    {
        await authService.DeleteAllUsersAsync();

        return Ok(new { Message = "Пользователи были удалены"});
    }
}