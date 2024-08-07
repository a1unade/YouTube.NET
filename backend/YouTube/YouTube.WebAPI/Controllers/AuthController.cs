using Microsoft.AspNetCore.Mvc;
using YouTube.Application.Interfaces;
using YouTube.Application.Interfaces.Repositories;
using YouTube.Domain.Entities;

namespace YouTube.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IDbContext _context;
    private readonly IGenericRepository<Category> _caRepository;

    public AuthController(IDbContext context, IGenericRepository<Category> caRepository)
    {
        _context = context;
        _caRepository = caRepository;
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

    [HttpPost("[action]")]
    public async Task<IActionResult> TestGenericRepositoryAddEntity(CancellationToken cancellationToken)
    {
        var ca = new Category
        {
            Id = Guid.NewGuid(),
            Name = "HIU"
        };

        await _caRepository.Add(ca, cancellationToken);

        return Ok();
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