using AutoMapper;
using Users_account_management.Domain_Models;
using Users_account_management.Domain_Services;
using Users_account_management.DTO;

namespace Users_account_management.Services
{
    public class UserService : IUserService
    {
        private readonly UserDbContext dbContext;
        private readonly IMapper mapper;

        public UserService(UserDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }
        public bool АuthorizationByEmail(string username,string password)
        { 
            var user=dbContext.Users.FirstOrDefault(x => x.Email == username && x.Password==password);
            return user!=null;
        }
        public bool АuthorizationByPhoneNumber(string username,string password)
        {
            var user = dbContext.Users.FirstOrDefault(x => x.PhoneNumber == username && x.Password == password);
            return user != null;
        }
        public bool RegisterUser(UserDto userDto)
        {
            if (dbContext.Users.Any(u => u.Email == userDto.Email || u.PhoneNumber == userDto.PhoneNumber))
            {
                return false;
            }
            var newUser = mapper.Map<User>(userDto);
            dbContext.Users.Add(newUser);
            dbContext.SaveChanges();
            
            return true;
        }

        public User GetUserById(int userId)
        {
            return dbContext.Users.FirstOrDefault(u => u.Id == userId);
        }

        public List<User> GetAllUsers()
        {
            return dbContext.Users.ToList();
        }

        public User GetUserByEmail(string email)
        {
            return dbContext.Users.FirstOrDefault(u => u.Email == email);
        }

        public User GetUserByPhoneNumber(string phoneNumber)
        {
            return dbContext.Users.FirstOrDefault(u => u.PhoneNumber == phoneNumber);
        }
    }
}
