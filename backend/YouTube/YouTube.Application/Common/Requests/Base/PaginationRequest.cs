namespace YouTube.Application.Common.Requests.Base;

public class PaginationRequest
{
    public PaginationRequest()
    {
    }

    public PaginationRequest(PaginationRequest request)
    {
        Page = request.Page;
        Size = request.Size;
    }

    public int Page { get; set; }

    public int Size { get; set; }
}