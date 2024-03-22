using Microsoft.AspNetCore.Identity;

namespace Users_account_management.Domain_Models
{
    public class User 
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
    }
}
