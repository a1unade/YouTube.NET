using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace YouTube.Infrastructure.Options;

public class AuthOptions
{
    public string Issuer { get; set; } = null!;
    public string Audience { get; set; } = null!;
    public string SecretKey { get; set; } = null!;
    
    public int AccessTokenActivityDays { get; set; }
    
    public int RefreshTokenActivityDays { get; set; }

    public SymmetricSecurityKey GetSymmetricSecurityKey() => 
        new (Encoding.UTF8.GetBytes(SecretKey));
}