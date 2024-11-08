namespace YouTube.Application.Common.Responses.Channel;

/// <summary>
/// Ответ о подробной информации канала
/// </summary>
public class ChannelLinksResponse : BaseResponse
{
    /// <summary>
    /// Ссылки
    /// </summary>
    public List<string> Links { get; set; } = default!;
    
    /// <summary>
    /// Кол-во видео
    /// </summary>
    public int Videos { get; set; }
    
    /// <summary>
    /// Кол-во подписчиков
    /// </summary>
    public int Subscribers { get; set; }
    
    /// <summary>
    /// Кол-во просмотров
    /// </summary>
    public int Views { get; set; }

    /// <summary>
    /// Страна
    /// </summary>
    public string Country { get; set; } = default!;
}