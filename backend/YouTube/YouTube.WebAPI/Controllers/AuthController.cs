using MediatR;
using Microsoft.AspNetCore.Mvc;
using YouTube.Domain.Entities;
using YouTube.Persistence.Contexts;

namespace YouTube.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ApplicationDbContext _context;

    public AuthController(IMediator mediator,ApplicationDbContext context)
    {
        _mediator = mediator;
        _context = context;
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> GetTwo(CancellationToken cancellationToken)
    {
        _context.Categories.Add(new Category
        {
            Id = Guid.NewGuid(),
            Name = "hui"
        });
        await _context.SaveChangesAsync(cancellationToken);
        return
            Ok(2);
    }

    [HttpDelete("[action]")]
    public async Task<IActionResult> Delete(CancellationToken cancellationToken)
    {
        var t = _context.Categories.Where(x => x.Name == "hui").ToList();
        
        _context.Categories.RemoveRange(t);

        await _context.SaveChangesAsync(cancellationToken);

        return Ok();
    }
}