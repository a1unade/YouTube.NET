using YouTube.Domain.Entities;

namespace YouTube.Application.Interfaces.Repositories;

public interface IUserRepository
{
    Task<User?> FindById(Guid id, CancellationToken cancellationToken);
    Task<User?> FindByEmail(string email, CancellationToken cancellationToken);
}