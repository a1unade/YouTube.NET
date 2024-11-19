namespace YouTube.Infrastructure.Options;

/// <summary>
/// Конфигурация для RabbitMq
/// </summary>
public class RabbitOptions
{
    /// <summary>
    /// Имя пользователя
    /// </summary>
    public string Username { get; set; } = default!;

    /// <summary>
    /// Пароль
    /// </summary>
    public string Password { get; set; } = default!;

    /// <summary>
    /// Хост
    /// </summary>
    public string Hostname { get; set; } = default!;
}