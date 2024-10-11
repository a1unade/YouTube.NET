namespace YouTube.Application.Common.Responses.User;

public class UserEmailResponse : BaseResponse
{
    public bool NewUser { get; set; }
    
    public bool Confirmation { get; set; }
    
}