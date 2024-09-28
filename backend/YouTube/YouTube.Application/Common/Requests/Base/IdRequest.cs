namespace YouTube.Application.Common.Requests.Base;

public class IdRequest
{
    public IdRequest()
    {
        
    }

    public IdRequest(IdRequest requests)
    {
        Id = requests.Id;
    }
    
    public string? Id { get; set; }
}