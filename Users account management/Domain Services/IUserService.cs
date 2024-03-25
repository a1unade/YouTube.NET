using Users_account_management.Domain_Models;
using Users_account_management.DTO;

namespace Users_account_management.Domain_Services
{
    public interface IUserService
    {
        bool RegisterUser(UserDto user);
        User GetUserById(int userId);
        List<User> GetAllUsers();
        User GetUserByEmail(string email);
        User GetUserByPhoneNumber(string phoneNumber);
        bool АuthorizationByEmail(string username, string email);
        bool АuthorizationByPhoneNumber(string username , string phoneNumber);
    }
}
