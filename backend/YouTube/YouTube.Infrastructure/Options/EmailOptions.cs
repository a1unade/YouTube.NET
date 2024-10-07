namespace YouTube.Infrastructure.Options;

public class EmailOptions
{
    /// <summary>
    /// Хост
    /// </summary>
    public string Host { get; set; } = default!;
    
    /// <summary>
    /// Порт
    /// </summary>
    public int Port { get; set; }
    
    /// <summary>
    /// Адрес
    /// </summary>
    public string EmailAddress { get; set; } = default!;
    
    /// <summary>
    /// Пароль
    /// </summary>
    public string Password { get; set; } = default!;
}