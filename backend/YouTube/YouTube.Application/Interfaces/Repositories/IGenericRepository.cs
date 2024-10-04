
using System.Linq.Expressions;

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
    /// <returns></returns>
    IQueryable<T> GetAll();

    /// <summary>
    /// Получить сущность
    /// </summary>
    /// <param name="predicate">Условие получения</param>
    /// <returns></returns>
    IQueryable<T> Get(Expression<Func<T, bool>> predicate);

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