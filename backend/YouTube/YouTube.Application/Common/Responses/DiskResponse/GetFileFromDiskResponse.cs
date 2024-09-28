namespace YouTube.Application.Common.Responses.DiskResponse;

public class GetFileFromDiskResponse : BaseResponse
{
    public double Size { get; set; }
    
    public string? PathInDisk { get; set; }
    
    public string? File { get; set; }
    
    public LinkResponse? LinkResponse { get; set; }
}