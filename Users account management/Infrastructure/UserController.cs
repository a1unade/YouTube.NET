using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Users_account_management.Domain_Models;
using Users_account_management.Domain_Services;
using Users_account_management.DTO;
namespace Users_account_management.Infrastructure
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly string _key = "VDcyNjM3ODYyMDY3MzQ1OTI2MzU1MDczNTY1Njc0NTI2NzQ1NzA2MzQ2MjY0NjA2MjM1Mzc3MDU0NjA1NTU2MzQ2NTc4NjE2MjU4NzMxNDY3Nzc3NjE0NjI2NDYwNjUzNDU1Njc4Njg3NDU0NzY1MzQxNDMzNDUyNjY2NTQzNDMwNTU2MzY0NzQ3NjU0NjA3MzU2MjYzNjU3NjM0NTUzNzQ2NzUxNjM0Mzg2NTI2NjQyNjE3NDU2NzE1MzY0MjY5MzY0MzU1Mzc0MzYzNDU0NjA0NzYzNjQxNjE2MzUzNzI";
        private readonly IUserService userService;

        public UserController(IUserService userManagementService)
        {
            this.userService = userManagementService;
        }

        [HttpPost]
        public IActionResult RegisterUser([FromBody] UserDto user)
        {
            var res=userService.RegisterUser(user);
            if(res)
                return Ok();
            return BadRequest(res);
        }
        [HttpGet("{userId}")]
        public IActionResult GetUserById(int userId)
        {
            var user = userService.GetUserById(userId);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpGet("auth/email/{email}/{password}")]
        public IActionResult GetRightsByEmail(string email, string password)
        {
            var auth = userService.АuthorizationByEmail(email, password);
            if (auth)
            {
                var token = GenerateToken(email); 
                Response.Cookies.Append("auth_token", token, new CookieOptions
                {
                    HttpOnly = true, 
                });
                return Ok(new { token });
            }
            return NotFound();
        }

        [HttpGet("auth/phone/{phoneNumber}/{password}")]
        public IActionResult GetRightsByPhoneNumber(string phoneNumber, string password)
        {
            var auth = userService.АuthorizationByPhoneNumber(phoneNumber, password);
            if (auth)
            {
                var token = GenerateToken(phoneNumber); 
                Response.Cookies.Append("auth_token", token, new CookieOptions
                {
                    HttpOnly = true, 
                });
                return Ok(new { token });
            }

            return NotFound();
        }
        [Authorize] 
        [HttpGet("email/{email}")]
        public IActionResult GetUserByEmail(string email)
        {
            var user = userService.GetUserByEmail(email);
            if (user == null)
                return NotFound();

            return Ok(user);
        }
        [Authorize] 
        [HttpGet("phone/{phoneNumber}")]
        public IActionResult GetUserByPhoneNumber(string phoneNumber)
        {
            var user = userService.GetUserByPhoneNumber(phoneNumber);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = userService.GetAllUsers();
            return Ok(users);
        }
        private string GenerateToken(string email)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, email),
            };

            var jwt = new JwtSecurityToken(
                issuer: "your_issuer",
                audience: "your_audience",
                claims: claims,
                expires: DateTime.UtcNow.AddDays(7),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key)), SecurityAlgorithms.HmacSha256Signature)
            );

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.WriteToken(jwt);
    
            return token;
        }
    }

}
