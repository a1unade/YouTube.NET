using Microsoft.AspNetCore.Identity;
using YouTube.Application.DTOs;

namespace YouTube.Application.Interfaces;

public interface IAuthService
{
    Task<IdentityResult> RegisterAsync(RegisterDto registerDto);
}