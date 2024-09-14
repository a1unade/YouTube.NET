using MediatR;
using Microsoft.AspNetCore.Mvc;
using YouTube.Application.Common.Requests.Auth;
using YouTube.Application.Features.Auth.Auth;
using YouTube.Application.Interfaces;
using YouTube.Application.Interfaces.Repositories;
using YouTube.Domain.Entities;

namespace YouTube.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IDbContext _context;
    private readonly IGenericRepository<Channel> _caRepository;
    private readonly IMediator _mediator;

    public AuthController(IDbContext context, IGenericRepository<Channel> caRepository, IMediator mediator)
    {
        _context = context;
        _caRepository = caRepository;
        _mediator = mediator;
    }

    

    [HttpDelete("[action]")]
    public async Task<IActionResult> DeleteUser(CancellationToken cancellationToken)
    {

        foreach (var user in _context.Users)
        {
            _context.Users.Remove(user);
        }

        foreach (var userInfo in _context.UserInfos)
        {
            _context.UserInfos.Remove(userInfo);
        }

        await _context.SaveChangesAsync(cancellationToken);

        return Ok();
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> AuthUser(AuthRequest request, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var result = await _mediator.Send(new AuthCommand(request), cancellationToken);
        
        if (result.IsSuccessfully)
            return Ok(result);

        return NotFound(result);
    }

    // [HttpPost("[action]")]
    // public async Task<IActionResult> LoginUser(LoginRequest request, CancellationToken cancellationToken)
    // {
    //     
    // }

    [HttpPost("[action]")]
    public async Task<IActionResult> TestGenericRepositoryAddEntity(CancellationToken cancellationToken)
    {
        try
        {
            var userInfo = new UserInfo
            {
                Id = Guid.NewGuid(),
                Name = "Ffff",
                Surname = "Ffff",
                BirthDate = default,
                Gender = "null"
            };
            var user = new User
            {
                Id = Guid.NewGuid(),
                UserName = "Bulat",
                Email = "Bulat2004@gmail.com",
                EmailConfirmed = false,
                PasswordHash = null,
                UserInfoId = userInfo.Id,
                UserInfo = userInfo
            };
            userInfo.User = user;
            
            var ca = new Channel
            {
                Id = Guid.NewGuid(),
                Name = "HIU",
                Description = "",
                CreateDate = DateOnly.FromDateTime(DateTime.Today),
                SubCount = 0,
                User = user,
                UserId = user.Id
            };

            await _caRepository.Add(ca, cancellationToken);

            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, "Pizda");
        }
    }

    [HttpDelete("[action]")]
    public async Task<IActionResult> TestGenericRepositoryDelete(CancellationToken cancellationToken)
    {
        var entity = await _caRepository.GetById(Guid.Parse("afdd48d4-0193-4f12-98bf-9ca4ff3e3dfd"), cancellationToken);
        if (entity == null)
        {
            return Ok("empty");
        }
        await _caRepository.Remove(entity, cancellationToken);

        return Ok("Ok");
    }
}