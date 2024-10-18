namespace YouTube.Application.Common.Requests.Video;

public class VideoPaginationRequest
{
    public VideoPaginationRequest()
    {
        
    }

    public VideoPaginationRequest(VideoPaginationRequest request)
    {
        Page = request.Page;
        Size = request.Size;
        Category = request.Category;
        Sort = request.Sort;
    }
    
    public int Page { get; set; }
    
    public int Size { get; set; }

    public string? Category { get; set; }

    public string? Sort { get; set; }
}