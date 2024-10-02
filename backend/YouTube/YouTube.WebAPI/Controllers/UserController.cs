using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YouTube.Application.Common.Requests.User;
using YouTube.Application.Features.Email.ForgotPasswordSendEmail;
using YouTube.Application.Interfaces;
using YouTube.Application.Interfaces.Repositories;

namespace YouTube.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IDbContext _context;
    private readonly IUserRepository _userRepository;

    public UserController(IMediator mediator, IDbContext context, IUserRepository userRepository)
    {
        _mediator = mediator;
        _context = context;
        _userRepository = userRepository;
    }


    [HttpPost("ForgotPassword")]
    public async Task<IActionResult> ForgotPassword(EmailRequest request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new ForgotPasswordSendEmailCommand(request), cancellationToken);

        if (result.IsSuccessfully)
            return Ok(result);

        return NotFound(result);
    }

    [HttpGet("GetAllUser")]
    public async Task<IActionResult> GetAllUser(CancellationToken cancellationToken)
    {
        var user = await _context.Users.Include(x => x.UserInfo).ToListAsync(cancellationToken);

        return Ok(user);
    }

    [HttpGet("GetUser")]
    public async Task<IActionResult> GetUser(Guid id, CancellationToken cancellationToken)
    {
        var user = await _userRepository.FindById(id, cancellationToken);

        return Ok(user);
    }
}