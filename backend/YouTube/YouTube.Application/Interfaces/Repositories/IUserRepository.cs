using YouTube.Domain.Entities;

namespace YouTube.Application.Interfaces.Repositories;

/// <summary>
/// Репозиторий для работы с пользователем
/// </summary>
public interface IUserRepository
{
    /// <summary>
    /// Достать пользователя по Id
    /// </summary>
    /// <param name="id">Id</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>User or null</returns>
    Task<User?> FindById(Guid id, CancellationToken cancellationToken);
    
    /// <summary>
    /// Достать пользователя по почте
    /// </summary>
    /// <param name="email">Почта</param>
    /// <param name="cancellationToken">СancellationToken</param>
    /// <returns>User or null</returns>
    Task<User?> FindByEmail(string email, CancellationToken cancellationToken);

    /// <summary>
    /// Получить пользователя с его каналом
    /// </summary>
    /// <param name="id">Id пользователя</param>
    /// <param name="cancellationToken">СancellationToken</param>
    /// <returns></returns>
    Task<User?> GetUserWithChannel(Guid id, CancellationToken cancellationToken);
}