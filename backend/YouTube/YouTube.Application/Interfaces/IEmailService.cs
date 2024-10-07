namespace YouTube.Application.Interfaces;

public interface IEmailService
{
    /// <summary>
    /// Отправка письма
    /// </summary>
    /// <param name="email">Почта юзера</param>
    /// <param name="subject">Заголовок</param>
    /// <param name="messageBody">Сообщение</param>
    /// <returns></returns>
    Task SendEmailAsync(string email, string subject, string messageBody);
    
    /// <summary>
    /// Генерация кода
    /// </summary>
    /// <returns>Код</returns>
    string GenerateRandomCode();
}