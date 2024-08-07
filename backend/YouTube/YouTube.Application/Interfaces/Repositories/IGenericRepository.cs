
namespace YouTube.Application.Interfaces.Repositories;

public interface IGenericRepository<T> where T : class
{
    Task Add(T entity, CancellationToken cancellationToken);

    Task<IEnumerable<T>> GetAll(CancellationToken cancellationToken);

    IEnumerable<T> Get(Func<T, bool> predicate, CancellationToken cancellationToken);

    Task<T?> GetById(Guid id, CancellationToken cancellationToken);
    
    Task Remove(T item, CancellationToken cancellationToken);

    Task RemoveById(Guid id, CancellationToken cancellationToken);
    
}