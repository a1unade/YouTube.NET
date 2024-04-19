using YouTube.Application.Common.Responses;
using YouTube.Application.DTOs.Auth;

namespace YouTube.Application.Interfaces;

public interface IAuthService
{
    /// <summary>
    /// Регистрация пользователя
    /// </summary>
    Task<UserResponse> RegisterAsync(RegisterDto registerDto);
    
    /// <summary>
    /// Подтверждение почты
    /// </summary>
    Task<UserResponse> ConfirmEmailAsync(string userId, string token);

    /// <summary>
    /// Удаление пользователей из бд (для проверки функционала)
    /// </summary>
    Task DeleteAllUsersAsync();

    // /// <summary>
    // /// Смена пароля
    // /// </summary>
    // Task<UserResponse> ChangePasswordAsync(ChangePasswordDto changePasswordDto);
    //
    // /// <summary>
    // /// Повторная отправка ссылки для подтверждения почты
    // /// </summary>
    // Task<UserResponse> SendConfirmationAsync(UserEmailDto userDto);
}