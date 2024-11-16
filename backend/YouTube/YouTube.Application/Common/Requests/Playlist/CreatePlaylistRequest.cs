namespace YouTube.Application.Common.Requests.Playlist;
/// <summary>
/// Запрос на создание плейлиста
/// </summary>
public class CreatePlaylistRequest
{
    public CreatePlaylistRequest()
    {
        
    }
    
    public CreatePlaylistRequest(CreatePlaylistRequest request)
    {
        ChannelId = request.ChannelId;
        Name = request.Name;
        Description = request.Description;
        IsHidden = request.IsHidden;
        VideosId = request.VideosId;
    }
    /// <summary>
    /// Id пользователя
    /// </summary>
    public Guid ChannelId { get; set; }

    /// <summary>
    /// Название
    /// </summary>
    public string Name { get; set; } = default!;

    /// <summary>
    /// Описание
    /// </summary>
    public string? Description { get; set; }
    
    /// <summary>
    /// Открытый ли
    /// </summary>
    public bool IsHidden { get; set; }

    /// <summary>
    /// Ид видео которые будут в плейлисте
    /// </summary>
    public List<Guid> VideosId { get; set; } = default!;
}