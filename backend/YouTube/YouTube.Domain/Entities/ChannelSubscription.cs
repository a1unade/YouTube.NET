using YouTube.Domain.Common;

namespace YouTube.Domain.Entities;

/// <summary>
/// Подписки на каналы
/// </summary>
public class ChannelSubscription 
{
    /// <summary>
    /// Id Подписчика
    /// </summary>
    public Guid SubscriberChannelId { get; set; }

    /// <summary>
    /// Канал Подписчика
    /// </summary>
    public Channel Subscriber { get; set; } = default!;

    /// <summary>
    /// Id канала который подписался
    /// </summary>
    public Guid  SubscribedToChannelId { get; set; }

    /// <summary>
    /// Канал на который подписались
    /// </summary>
    public Channel SubscribedTo { get; set; } = default!;
}