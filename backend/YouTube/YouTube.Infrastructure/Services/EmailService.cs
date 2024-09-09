using MimeKit;
using MailKit.Net.Smtp;
using YouTube.Application.Interfaces;

namespace YouTube.Infrastructure.Services;

public class EmailService: IEmailService
{
    private const string Host = "smtp.yandex.com";
    private const int Port = 465;
    
    public async Task SendEmailAsync(string email, string subject, string messageBody)
    {
        try
        {
            using var smtpClient = new SmtpClient();

            await smtpClient.ConnectAsync(Host, Port, true);
            await smtpClient.AuthenticateAsync("jinx.httpserver@yandex.ru", "kfgxrkizhidqhnyd");
            await smtpClient.SendAsync(GenerateMessage(email, subject, messageBody));
            await smtpClient.DisconnectAsync(true);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to send mail message: {ex.Message}");
        }
    }

    private MimeMessage GenerateMessage(string email, string subject, string messageBody)
    {
        return new MimeMessage
        {
            From = {new MailboxAddress("YouTube", "jinx.httpserver@yandex.ru")},
            To = {new MailboxAddress("", email)},
            Subject = subject,
            Body = new BodyBuilder { HtmlBody = messageBody }.ToMessageBody()
        };
    }
    
    public string GenerateRandomCode() => new Random().Next(100000, 999999).ToString();
}