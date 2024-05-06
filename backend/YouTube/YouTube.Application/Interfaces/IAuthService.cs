using YouTube.Application.Common.Responses;
using YouTube.Application.DTOs.Auth;

namespace YouTube.Application.Interfaces;

public interface IAuthService
{
    /// <summary>
    /// Регистрация пользователя
    /// </summary>
    Task<AuthResponse> RegisterAsync(RegisterDto registerDto);
    
    /// <summary>
    /// Подтверждение почты
    /// </summary>
    Task<AuthResponse> ConfirmEmailAsync(string userId, string token);

    /// <summary>
    /// Удаление пользователей из бд (для проверки функционала)
    /// </summary>
    Task DeleteAllUsersAsync();

    /// <summary>
    /// Получение пользователя по адресу электронной почты
    /// </summary>
    /// <param name="email">Адрес электронной почты пользователя</param>
    /// <returns>Пользователь при success</returns>
    Task<UserResponse> GetUserByEmailAsync(string email);

    Task<AuthResponse> SignInAsync(LoginDto loginDto);

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