using Microsoft.AspNetCore.Mvc;
using Users_account_management.Domain_Models;
using Users_account_management.Domain_Services;
using Users_account_management.DTO;
namespace Users_account_management.Infrastructure
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
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
                return Ok();

            return NotFound();
        }

        [HttpGet("auth/phone/{phoneNumber}/{password}")]
        public IActionResult GetRightsByPhoneNumber(string phoneNumber, string password)
        {
            var auth = userService.АuthorizationByPhoneNumber(phoneNumber, password);
            if (auth)
                return Ok();

            return NotFound();
        }
        [HttpGet("email/{email}")]
        public IActionResult GetUserByEmail(string email)
        {
            var user = userService.GetUserByEmail(email);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

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
    }

}
