using Microsoft.EntityFrameworkCore;
using YouTube.Application.Interfaces;
using YouTube.Application.Interfaces.Repositories;
using YouTube.Domain.Entities;

namespace YouTube.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IDbContext _context;

    public UserRepository(IDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetById(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Users.FirstOrDefaultAsync(x => x.Id == id, cancellationToken) ?? null;
    }

    public async Task<User?> GetUserByEmail(string email, CancellationToken cancellationToken)
    {
        return await _context.Users.FirstOrDefaultAsync(x => x.Email == email, cancellationToken) ?? null;
    }
}