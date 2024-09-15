using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YouTube.Application.Common.Requests.User;
using YouTube.Application.Features.User.ForgotPassword;
using YouTube.Application.Interfaces;

namespace YouTube.WebAPI.Controllers;
[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IDbContext _context;

    public UserController(IMediator mediator,IDbContext context)
    {
        _mediator = mediator;
        _context = context;
    }
    

    [HttpPost("[action]")]
    public async Task<IActionResult> ForgotPassword(EmailRequest request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new ForgotPasswordCommand(request), cancellationToken);

        if (result.IsSuccessfully)
            return Ok(result);

        return NotFound(result);
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> GetAllUser(CancellationToken cancellationToken)
    {
        var user = await _context.Users.Include(x => x.UserInfo).ToListAsync(cancellationToken);

        return Ok(user);
    }
}