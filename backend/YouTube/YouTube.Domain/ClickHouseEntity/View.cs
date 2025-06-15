using YouTube.Domain.Common;

namespace YouTube.Domain.ClickHouseEntity;

public class View : BaseEntity
{
    public string VideoName { get; set; } = null!;
    
    public Guid VideoId { get; set; }
    
    public Guid ChannelId { get; set; }
    
    public long ViewCount { get; set; }
}