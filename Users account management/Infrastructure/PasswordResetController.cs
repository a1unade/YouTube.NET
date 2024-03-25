using Microsoft.AspNetCore.Mvc;
using Users_account_management.Domain_Services;

namespace Users_account_management.Infrastructure
{
    [Route("api/[controller]")]
    [ApiController]
    public class PasswordResetController : ControllerBase
    {
        private readonly IPasswordResetService passwordResetService;

        public PasswordResetController(IPasswordResetService passwordResetService)
        {
            this.passwordResetService = passwordResetService;
        }

        [HttpPost("request")]
        public IActionResult RequestPasswordReset([FromBody] string email)
        {
            if (passwordResetService.RequestPasswordReset(email))
                return Ok();
            else
                return BadRequest("Failed to request password reset.");
        }

        [HttpPost("reset")]
        public IActionResult ResetPassword([FromBody] PasswordResetRequest request)
        {
            if (passwordResetService.ResetPassword(request.Email, request.Code, request.NewPassword))
                return Ok();
            else
                return BadRequest("Failed to reset password.");
        }

        public class PasswordResetRequest
        {
            public string Email { get; set; }
            public string Code { get; set; }
            public string NewPassword { get; set; }
        }
    }
}