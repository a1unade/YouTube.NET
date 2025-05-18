namespace YouTube.Application.Common.Requests.Channel;

public class SubscribeToChannelRequest
{
    public SubscribeToChannelRequest()
    {
        
    }

    public SubscribeToChannelRequest(SubscribeToChannelRequest request)
    {
        Id = request.Id;
        ChannelToSubsId = request.ChannelToSubsId;
    }
    
    public Guid Id { get; set; }
    
    public Guid ChannelToSubsId { get; set; }
}