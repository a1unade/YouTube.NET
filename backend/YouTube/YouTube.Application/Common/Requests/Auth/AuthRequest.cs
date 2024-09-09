using System.ComponentModel.DataAnnotations;

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
    }
    
    public string Password { get; set; }
    
    public string Email { get; set; }
    
    public string Name { get; set; }
    
    public string SurName { get; set; }
    
    public DateTime DateOfBirth { get; set; }
    
    public string Gender { get; set; }
    
}