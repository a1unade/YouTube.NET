using System.Text.Json.Serialization;
using YouTube.Domain.Common;

namespace YouTube.Domain.Entities;

/// <summary>
/// История сообщений
/// </summary>
public class ChatHistory : BaseEntity
{
    /// <summary>
    /// Дата начала чата
    /// </summary>
    public DateOnly StartDate { get; set; }
    
    /// <summary>
    /// Ид Пользователя
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Пользователь
    /// </summary>
    [JsonIgnore]
    public User User { get; set; } = default!;
    
    /// <summary>
    /// История чата
    /// </summary>
    public ICollection<ChatMessage> ChatMessages { get; set; } = default!;
}