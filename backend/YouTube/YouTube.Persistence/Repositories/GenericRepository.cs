using Microsoft.EntityFrameworkCore;
using YouTube.Application.Interfaces;
using YouTube.Application.Interfaces.Repositories;

namespace YouTube.Persistence.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly IDbContext _context;

    private readonly DbSet<T> _set;

    public GenericRepository(IDbContext context)
    {
        _context = context;
        _set = context.Set<T>();
    }

    public async Task Add(T entity, CancellationToken cancellationToken)
    {
        await _set.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<T>> GetAll(CancellationToken cancellationToken)
    {
        return await _set.AsNoTracking().ToListAsync(cancellationToken);
    }
    //TODO Переделать на IQueryable
    public IEnumerable<T> Get(Func<T, bool> predicate, CancellationToken cancellationToken)
    {
        return _set.AsNoTracking().Where(predicate).ToList();
    }

    public async Task<T?> GetById(Guid id, CancellationToken cancellationToken)
    {
        return await _set.FindAsync(id, cancellationToken) ?? null;
    }

    public async Task Remove(T item, CancellationToken cancellationToken)
    {
        _set.Remove(item);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoveById(Guid id, CancellationToken cancellationToken)
    {
        var entity = _set.FindAsync(id, cancellationToken).Result;
        if (entity != null)
        {
            _set.Remove(entity);
        }
        await _context.SaveChangesAsync(cancellationToken);
    }
}