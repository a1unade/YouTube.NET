namespace YouTube.Application.Common.Requests.Email;

public class CodeCheckForChangePasswordRequest
{
    public CodeCheckForChangePasswordRequest()
    {
        
    }

    public CodeCheckForChangePasswordRequest(CodeCheckForChangePasswordRequest request)
    {
        Code = request.Code;
        Email = request.Email;
    }

    public string Code { get; set; } = default!;

    public string Email { get; set; } = default!;
}