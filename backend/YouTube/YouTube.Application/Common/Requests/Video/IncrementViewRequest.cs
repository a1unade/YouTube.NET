namespace YouTube.Application.Common.Requests.Video;

public class IncrementViewRequest
{
    public IncrementViewRequest()
    {
        
    }

    public IncrementViewRequest(IncrementViewRequest request)
    {
        VideoId = request.VideoId;
    }
    
    public Guid VideoId { get; set; }
}