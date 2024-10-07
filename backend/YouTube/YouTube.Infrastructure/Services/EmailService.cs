using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using YouTube.Application.Interfaces;

namespace YouTube.Infrastructure.Services;

public class EmailService: IEmailService
{
    private readonly IConfigurationSection _configurationSection;

    public EmailService(IConfiguration configuration)
    {
        _configurationSection = configuration.GetSection("EmailSettings");
    }
    
    public async Task SendEmailAsync(string email, string subject, string messageBody)
    {
        try
        {
            using var smtpClient = new SmtpClient();

            await smtpClient.ConnectAsync(_configurationSection["Host"], int.Parse(_configurationSection["Port"]!), true);
            await smtpClient.AuthenticateAsync(_configurationSection["EmailAddress"], _configurationSection["Password"]);
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
            From = {new MailboxAddress("YouTube", _configurationSection["EmailAddress"])},
            To = {new MailboxAddress("", email)},
            Subject = subject,
            Body = new BodyBuilder { HtmlBody = messageBody }.ToMessageBody()
        };
    }
    
    public string GenerateRandomCode() => new Random().Next(100000, 999999).ToString();
}