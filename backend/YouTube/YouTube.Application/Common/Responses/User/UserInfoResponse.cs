namespace YouTube.Application.Common.Responses.User;

public class UserInfoResponse : BaseResponse
{
    public string Name { get; set; } = default!;

    public string SurName { get; set; } = default!;

    public string Email { get; set; } = default!;

    public string UserName { get; set; } = default!;
    
    public bool IsPremium { get; set; }
}