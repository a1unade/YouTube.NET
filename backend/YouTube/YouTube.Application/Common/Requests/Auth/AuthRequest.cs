namespace YouTube.Application.Common.Requests.Auth;

public class AuthRequest
{
    public AuthRequest()
    {
        
    }
    public AuthRequest(AuthRequest request)
    {
        Password = request.Password;
        Email = request.Email;
        Name = request.Name;
        SurName = request.SurName;
        DateOfBirth = request.DateOfBirth;
        Gender = request.Gender;
        Country = request.Country;
    }

    public string Password { get; set; } = default!;
    
    public string Email { get; set; } = default!;
    
    public string Name { get; set; } = default!;
    
    public string SurName { get; set; } = default!;
    
    public DateTime DateOfBirth { get; set; }
    
    public string Gender { get; set; } = default!;

    public string Country { get; set; } = default!;
}