using MediatR;
using Microsoft.AspNetCore.Mvc;
using YouTube.Application.Interfaces;
using YouTube.Domain.Entities;

namespace YouTube.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IDbContext _context;

    public AuthController(IMediator mediator, IDbContext context)
    {
        _mediator = mediator;
        _context = context;
    }

    [HttpPost]
    public  IActionResult GetTwo()
    {
        _context.Categories.Add(new Category
        {
            Id = Guid.NewGuid(),
            Name = "hui"
        });
        return
            Ok(2);
    }
}