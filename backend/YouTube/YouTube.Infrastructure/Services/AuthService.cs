using Microsoft.AspNetCore.Identity;
using YouTube.Application.DTOs;
using YouTube.Application.Interfaces;
using YouTube.Domain.Entities;

namespace YouTube.Infrastructure.Services;

public class AuthService(UserManager<User> userManager) : IAuthService
{
    public async Task<IdentityResult> RegisterAsync(RegisterDto registerDto)
    {
        UserInfo userInfo = new UserInfo
        {
            Nickname = registerDto.Nickname
        };

        User user = new User
        {
            UserName = registerDto.Email,
            Email = registerDto.Email,
            UserInfo = userInfo
        };

        IdentityResult result = await userManager.CreateAsync(user, registerDto.Password);

        return result;
    }
}