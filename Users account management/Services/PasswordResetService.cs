using Users_account_management.Domain_Services;

using StackExchange.Redis;
using Microsoft.EntityFrameworkCore;


namespace Users_account_management.Services
{
    public class PasswordResetService : IPasswordResetService
    {
        private readonly IDatabase redisCache;
        private readonly UserDbContext dbContext;

        public PasswordResetService( IConnectionMultiplexer redis, UserDbContext dbContext)
        {

            this.redisCache = redis.GetDatabase();
            this.dbContext = dbContext;
        }

        public bool RequestPasswordReset(string email)
        {
            var user =dbContext.Users.FirstOrDefault(u=>u.Email==email);
            if (user == null)  
                return false;
            string resetCode = GenerateResetCode();
    
            redisCache.StringSet(email, resetCode, TimeSpan.FromMinutes(5));

            SendResetCodeByEmail(email, resetCode);

            return true;
        }

        public bool ResetPassword(string email, string code, string newPassword)
        {

            if (redisCache.KeyExists(email))
            {
                string temporaryPassword = redisCache.StringGet(email);

                redisCache.KeyDelete(email);
                var user = dbContext.Users.FirstOrDefault(u => u.Email == email);
                user.Password = newPassword;
                dbContext.SaveChanges(); 


                Console.WriteLine($"New password for {email}: {newPassword}");
                return true;
            }

            return false; 
        }

        private string GenerateResetCode()
        {
            return Guid.NewGuid().ToString().Substring(0, 5); ;
        }

        private void SendResetCodeByEmail(string email, string code)
        {
            Console.WriteLine($"Reset code for {email}: {code}");
        }
    }
}
