using Microsoft.AspNetCore.Mvc;
using YouTube.Application.Common.Enums;
using YouTube.Application.Common.Responses;
using YouTube.Application.DTOs.Auth;
using YouTube.Application.Interfaces;
using YouTube.Application.Interfaces.Repositories;

namespace YouTube.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IChannelRepository _channelRepository;

    public AuthController(IAuthService authService, IChannelRepository channelRepository)
    {
        _authService = authService;
        _channelRepository = channelRepository;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto registerDto, CancellationToken cancellationToken)
    {
        AuthResponse result = await _authService.RegisterAsync(registerDto);
        
        if (result.Type == UserResponseTypes.Success)
        {
            return Ok(result);
        }
        
        return BadRequest(result.Message);
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        AuthResponse result = await _authService.SignInAsync(loginDto);
        
        if (result.Type == UserResponseTypes.Success)
            return Ok(result);
        
        return BadRequest(result.Message);
    }
    
    [HttpGet("confirm-email")]
    public async Task<AuthResponse> ConfirmEmail(string userId, string token)
    {
        AuthResponse result = await _authService.ConfirmEmailAsync(userId, token);
        
        if (result.Type == UserResponseTypes.Success)
            return result;
        
        return result;
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> DeleteAllUsers()
    {
        await _authService.DeleteAllUsersAsync();

        return Ok(new { Message = "Пользователи были удалены"});
    }

    [HttpGet("getUserById")]
    public async Task<UserResponse> GetUserById(string userId) => 
        await _authService.GetUserByIdAsync(userId);

    [HttpPost("changeAvatar")]
    public async Task<AuthResponse> ChangeAvatar(ChangeAvatarDto changeAvatarDto) =>
        await _authService.ChangeUserAvatarAsync(changeAvatarDto.UserId, changeAvatarDto.AvatarId);
}