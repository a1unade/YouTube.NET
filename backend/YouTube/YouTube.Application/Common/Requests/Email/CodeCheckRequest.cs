namespace YouTube.Application.Common.Requests.Email;

public class CodeCheckRequest
{
    public CodeCheckRequest()
    {
    }

    public CodeCheckRequest(CodeCheckRequest request)
    {
        Code = request.Code;
        Email = request.Email;
    }

    public string Code { get; set; } = default!;

    public string Email { get; set; } = default!;
}