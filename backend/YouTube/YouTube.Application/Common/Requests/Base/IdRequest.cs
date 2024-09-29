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
    
    public Guid Id { get; set; }
}