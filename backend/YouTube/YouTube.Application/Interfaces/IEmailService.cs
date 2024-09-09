namespace YouTube.Application.Interfaces;

public interface IEmailService
{
    /// <summary>
    /// Отправка письма с кодом подтверждения на почту
    /// </summary>
    Task SendEmailAsync(string email, string subject, string messageBody);
    
    string GenerateRandomCode();
}