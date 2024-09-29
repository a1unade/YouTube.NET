namespace YouTube.Application.Common.Requests.Email;

public class CodeCheckRequest
{
    public CodeCheckRequest()
    {
        
    }
    public CodeCheckRequest(CodeCheckRequest request)
    {
        Code = request.Code;
        Id = request.Id;
    }
    
    public string Code { get; set; }
    
    public Guid Id { get; set; }
}