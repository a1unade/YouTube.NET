using System.Net;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using YouTube.Application.Common.Enums;
using YouTube.Application.Common.Messages.Error;
using YouTube.Application.Common.Messages.Success;
using YouTube.Application.Common.Responses;
using YouTube.Application.DTOs.Auth;
using YouTube.Application.Interfaces;
using YouTube.Domain.Entities;
using YouTube.Infrastructure.SignalR;
using YouTube.Persistence.Contexts;

namespace YouTube.Infrastructure.Services;

public class AuthService(UserManager<User> userManager, IEmailService emailSender, IHubContext<EmailConfirmationHub> hubContext, ApplicationDbContext dbContext) : IAuthService
{
    public async Task<AuthResponse> RegisterAsync(RegisterDto registerDto)
    {
        UserInfo userInfo = new UserInfo
        {
            Name = registerDto.Name,
            Surname = registerDto.Surname,
            BirthDate = registerDto.BirthDate,
            Gender = registerDto.Gender,
            AvatarId = 1
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
                $"http://localhost:5041/api/Auth/confirm-email?userId={user.Id}&token={WebUtility.UrlEncode(token)}";
            
            await emailSender.SendEmailAsync(user.Email, "Подтверждение почты", 
                $"Для подтверждения вашей электронной почты перейдите по <a href=\"{confirmationLink}\">ссылке</a>");

            return new AuthResponse { Type = UserResponseTypes.Success, Message = AuthSuccessMessages.RegisterSuccess };
        }
        else
        {
            User? foundUser = await userManager.FindByEmailAsync(registerDto.Email);
            
            if (foundUser != null)
            {
                bool emailConfirmed = await userManager.IsEmailConfirmedAsync(foundUser);

                if (emailConfirmed)
                    return new AuthResponse
                        { Type = UserResponseTypes.Error, Message = AuthErrorMessages.RegisterError };
                
                string token = await userManager.GenerateEmailConfirmationTokenAsync(foundUser);
                string confirmationLink =
                    $"http://localhost:5041/api/Auth/confirm-email?userId={user.Id}&token={WebUtility.UrlEncode(token)}";
            
                await emailSender.SendEmailAsync(user.Email, "Подтверждение почты", 
                    $"Для подтверждения вашей электронной почты перейдите по <a href=\"{confirmationLink}\">ссылке</a>");

                return new AuthResponse { Type = UserResponseTypes.Success, Message = AuthSuccessMessages.RegisterSuccess };
            }
        }

        return new AuthResponse { Type = UserResponseTypes.Error, Message = AuthErrorMessages.RegisterError };
    }

    public async Task<AuthResponse> ConfirmEmailAsync(string userId, string token)
    {
        User? user = await userManager.FindByIdAsync(userId);

        if (user == null)
            return new AuthResponse { Type = UserResponseTypes.Error, Message = AuthErrorMessages.UserNotFound };

        var result = await userManager.ConfirmEmailAsync(user, token);

        if (result.Succeeded)
        {
            await hubContext.Clients.All.SendAsync("ReceiveEmailConfirmationStatus", true);
            return new AuthResponse { Type = UserResponseTypes.Success, Message = AuthSuccessMessages.EmailConfirmed };
        }
            

        return new AuthResponse { Type = UserResponseTypes.Error, Message = AuthErrorMessages.EmailConfirmationError };
    }
    
    public async Task DeleteAllUsersAsync()
    {
        var users = await userManager.Users.ToListAsync();

        foreach (var user in users)
            await userManager.DeleteAsync(user);
    }
    
    public async Task<UserResponse> GetUserByEmailAsync(string email)
    {
        try
        {
            User? user = await userManager.Users.AsNoTracking()
                .Include(u => u.UserInfo)
                .Include(u => u.UserInfo.Avatar)
                .FirstOrDefaultAsync(u => u.Email == email);

            return new UserResponse
            {
                ResponseType = UserResponseTypes.Success,
                Email = user!.Email,
                Name = user.UserInfo.Name,
                Susrname = user.UserInfo.Surname,
                Gender = user.UserInfo.Gender,
                BirthDate = user.UserInfo.BirthDate.ToString("d"),
                Avatar = user.UserInfo.Avatar.Path
            };
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("GetUserByEmail: " + ex.Message);
            Console.ResetColor();
            
            return new UserResponse { ResponseType = UserResponseTypes.Error };
        }
    }

    public async Task<AuthResponse> SignInAsync(LoginDto loginDto)
    {
        User? user = await userManager.FindByEmailAsync(loginDto.Email);

        if (user != null)
        {
            bool isPasswordCorrect = await userManager.CheckPasswordAsync(user, loginDto.Password);

            if (isPasswordCorrect)
                return new AuthResponse
                    { Type = UserResponseTypes.Success, Message = AuthSuccessMessages.LoginSuccess, UserId = user.Id.ToString()};

            return new AuthResponse { Type = UserResponseTypes.Error, Message = AuthErrorMessages.LoginWrongPassword };
        }
        
        return new AuthResponse { Type = UserResponseTypes.Error, Message = AuthErrorMessages.UserNotFound };
    }
    
    public async Task<UserResponse> GetUserByIdAsync(string id)
    {
        try
        {
            User? user = await userManager.Users.AsNoTracking()
                .Include(u => u.UserInfo)
                .Include(u => u.UserInfo.Avatar)
                .FirstOrDefaultAsync(u => u.Id.ToString() == id);

            return new UserResponse
            {
                ResponseType = UserResponseTypes.Success,
                Email = user!.Email,
                Name = user.UserInfo.Name,
                Susrname = user.UserInfo.Surname,
                Gender = user.UserInfo.Gender,
                BirthDate = user.UserInfo.BirthDate.ToString("d"),
                Avatar = user.UserInfo.Avatar.Path
            };
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("GetUserById: " + ex.Message);
            Console.ResetColor();
            
            return new UserResponse { ResponseType = UserResponseTypes.Error };
        }
    }

    public async Task<AuthResponse> ChangeUserAvatarAsync(string userId, int avatarId)
    {
        // Найдем информацию о пользователе по его Id
        var userInfo = await dbContext.UserInfos.FirstOrDefaultAsync(u => u.UserId == Guid.Parse(userId));

        if (userInfo == null)
            return new AuthResponse { Type = UserResponseTypes.Error, Message = "UserInfo not found" };

        // Обновим AvatarId пользователя
        userInfo.AvatarId = avatarId;

        // Сохраним изменения
        await dbContext.SaveChangesAsync();

        return new AuthResponse { Type = UserResponseTypes.Success };
    }
}