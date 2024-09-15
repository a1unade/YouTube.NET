namespace YouTube.Application.Interfaces;

public interface IEmailService
{
    /// <summary>
    /// Отправка письма 
    /// </summary>
    Task SendEmailAsync(string email, string subject, string messageBody);
    
    string GenerateRandomCode();
}