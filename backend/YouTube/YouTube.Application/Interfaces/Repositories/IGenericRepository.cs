
namespace YouTube.Application.Interfaces.Repositories;

/// <summary>
/// Репозиторий для работы с сущностями
/// </summary>
/// <typeparam name="T">Сущность</typeparam>
public interface IGenericRepository<T> where T : class
{
    /// <summary>
    /// Добавить запись
    /// </summary>
    /// <param name="entity">Сущность</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns></returns>
    Task Add(T entity, CancellationToken cancellationToken);

    /// <summary>
    /// Получить все записи о сущности
    /// </summary>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns></returns>
    Task<IEnumerable<T>> GetAll(CancellationToken cancellationToken);

    /// <summary>
    /// Получить сущность
    /// </summary>
    /// <param name="predicate">Условие получения</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns></returns>
    IEnumerable<T> Get(Func<T, bool> predicate, CancellationToken cancellationToken);

    /// <summary>
    /// Получить сущность по Id
    /// </summary>
    /// <param name="id">Id</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns></returns>
    Task<T?> GetById(Guid id, CancellationToken cancellationToken);
    
    /// <summary>
    /// Удалить запись
    /// </summary>
    /// <param name="item">Сущность</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns></returns>
    Task Remove(T item, CancellationToken cancellationToken);

    /// <summary>
    /// Удалить сущность по Id
    /// </summary>
    /// <param name="id">Id</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns></returns>
    Task RemoveById(Guid id, CancellationToken cancellationToken);
}