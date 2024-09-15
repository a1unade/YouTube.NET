using YouTube.Domain.Entities;

namespace YouTube.Application.Interfaces.Repositories;

public interface IUserRepository
{
    Task<User?> GetById(Guid id, CancellationToken cancellationToken);
    Task<User?> GetUserByEmail(string email, CancellationToken cancellationToken);
}