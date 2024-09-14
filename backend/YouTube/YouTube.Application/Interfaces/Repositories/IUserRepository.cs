using YouTube.Domain.Entities;

namespace YouTube.Application.Interfaces.Repositories;

public interface IUserRepository
{
    Task<User?> GetUserByEmail(string email, CancellationToken cancellationToken);
}