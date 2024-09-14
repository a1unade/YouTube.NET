using MediatR;
using Microsoft.AspNetCore.Mvc;
using YouTube.Application.Common.Requests.Base;
using YouTube.Application.Common.Requests.User;
using YouTube.Application.Features.User.ForgotPassword;

namespace YouTube.WebAPI.Controllers;
[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> ForgotPassword(EmailRequest request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new ForgotPasswordCommand(request), cancellationToken);

        if (result.IsSuccessfully)
            return Ok(result);

        return NotFound(result);
    }
}