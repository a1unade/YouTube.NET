using System.Net;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using YouTube.Application.Common.Enums;
using YouTube.Application.Common.Messages.Error;
using YouTube.Application.Common.Messages.Success;
using YouTube.Application.Common.Responses;
using YouTube.Application.DTOs;
using YouTube.Application.DTOs.Auth;
using YouTube.Application.Interfaces;
using YouTube.Domain.Entities;

namespace YouTube.Infrastructure.Services;

public class AuthService(UserManager<User> userManager, IEmailService emailSender) : IAuthService
{
    public async Task<UserResponse> RegisterAsync(RegisterDto registerDto)
    {
        UserInfo userInfo = new UserInfo
        {
            Name = registerDto.Name,
            Surname = registerDto.Surname,
            BirthDate = registerDto.BirthDate,
            Gender = registerDto.Gender
        };

        User user = new User
        {
            UserName = registerDto.Email,
            Email = registerDto.Email,
            UserInfo = userInfo
        };

        IdentityResult result = await userManager.CreateAsync(user, registerDto.Password);

        if (result.Succeeded)
        {
            string token = await userManager.GenerateEmailConfirmationTokenAsync(user);
            string confirmationLink =
                $"https://localhost:7102/api/Auth/confirm-email?userId={user.Id}&token={WebUtility.UrlEncode(token)}";
            
            await emailSender.SendEmailAsync(user.Email, "Подтверждение почты", 
                $"Для подтверждения вашей электронной почты перейдите по <a href=\"{confirmationLink}\">ссылке</a>");

            return new UserResponse { Type = UserResponseTypes.Success, Message = AuthSuccessMessages.RegisterSuccess };
        }

        return new UserResponse { Type = UserResponseTypes.Error, Message = AuthErrorMessages.RegisterError };
    }

    public async Task<UserResponse> ConfirmEmailAsync(string userId, string token)
    {
        User? user = await userManager.FindByIdAsync(userId);

        if (user == null)
            return new UserResponse { Type = UserResponseTypes.Error, Message = AuthErrorMessages.UserNotFound };

        var result = await userManager.ConfirmEmailAsync(user, token);

        if (result.Succeeded)
            return new UserResponse { Type = UserResponseTypes.Success, Message = AuthSuccessMessages.EmailConfirmed };

        return new UserResponse { Type = UserResponseTypes.Error, Message = AuthErrorMessages.EmailConfirmationError };
    }
    
    public async Task DeleteAllUsersAsync()
    {
        var users = await userManager.Users.ToListAsync();

        foreach (var user in users)
            await userManager.DeleteAsync(user);
    }
    
    // TODO: фича для сброса пароля
    // public async Task<UserResponse> ChangePasswordAsync(ChangePasswordDto changePasswordDto)
    // {
    //     User? user = await userManager.FindByEmailAsync(changePasswordDto.Email);
    //
    //     if (user == null)
    //         return new UserResponse { Type = UserResponseTypes.Error, Message = AuthErrorMessages.UserNotFound };
    //     
    //     if (!user.EmailConfirmed)
    //         return new UserResponse { Type = UserResponseTypes.Error, Message = AuthErrorMessages.EmailNotConfirmed };
    //     
    //     //IdentityResult result = await userManager.ResetPasswordAsync(user, user.PasswordHash, changePasswordDto.NewPassword);
    //
    //     //if (result.Succeeded)
    //         //return new UserResponse { Type = UserResponseTypes.Success, Message = AuthSuccessMessages.PasswordChanged};
    //
    //     return new UserResponse { Type = UserResponseTypes.Error, Message = AuthErrorMessages.ChangePasswordError };
    // }

    // TODO: фича для повторной отправки письма с подтверждением почты
    // public async Task<UserResponse> SendConfirmationAsync(UserEmailDto userDto)
    // {
    //     User? user = await userManager.FindByEmailAsync(userDto.Email);
    //     
    //     if (user == null)
    //         return new UserResponse { Type = UserResponseTypes.Error, Message = AuthErrorMessages.UserNotFound };
    //
    //     string token = await userManager.GenerateEmailConfirmationTokenAsync(user);
    //     string confirmationLink =
    //         $"https://localhost:7102/api/Auth/confirm-email?userId={user.Id}&token={WebUtility.UrlEncode(token)}";
    //         
    //     await emailSender.SendEmailAsync(user.Email!, "Подтверждение почты", 
    //         $"Для подтверждения вашей электронной почты перейдите по <a href=\"{confirmationLink}\">ссылке</a>");
    //     
    //     return new UserResponse { Type = UserResponseTypes.Success, Message = AuthSuccessMessages.EmailConfirmMessageSend };
    // }
}