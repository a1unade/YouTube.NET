namespace YouTube.Application.DTOs.Auth;

public class ChangeAvatarDto
{
    /// <summary>
    /// ID пользователя
    /// </summary>
    public string UserId { get; set; }
    
    /// <summary>
    /// Id новой ававтарки
    /// </summary>
    public int AvatarId { get; set; }
}