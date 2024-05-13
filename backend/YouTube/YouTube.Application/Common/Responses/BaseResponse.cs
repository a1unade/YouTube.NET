namespace YouTube.Application.Common.Responses;

public class BaseResponse
{
    public bool IsSuccessfully { get; set; }
    
    public List<string>? Error { get; set; }
    
    public string? Message { get; set; }
}