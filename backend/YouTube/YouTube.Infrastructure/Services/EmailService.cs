using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using YouTube.Application.Interfaces;
using YouTube.Infrastructure.Options;

namespace YouTube.Infrastructure.Services;

public class EmailService: IEmailService
{

    private readonly EmailOptions _options;

    public EmailService(IConfiguration configuration)
    {
        _options = configuration.GetSection("EmailSettings").Get<EmailOptions>()!;
    }
    
    public async Task SendEmailAsync(string email, string subject, string messageBody)
    {
        try
        {
            using var smtpClient = new SmtpClient();
            await smtpClient.ConnectAsync(_options.Host, _options.Port, true);
            await smtpClient.AuthenticateAsync(_options.EmailAddress, _options.Password);
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
            From = {new MailboxAddress("YouTube", _options.EmailAddress)},
            To = {new MailboxAddress("", email)},
            Subject = subject,
            Body = new BodyBuilder { HtmlBody = messageBody }.ToMessageBody()
        };
    }
    
    public string GenerateRandomCode() => new Random().Next(100000, 999999).ToString();
}