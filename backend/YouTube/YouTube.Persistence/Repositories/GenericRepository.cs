using System.Linq.Expressions;
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

    public IQueryable<T> GetAll()
    {
        return _set.AsNoTracking();
    }

    public IQueryable<T> Get(Expression<Func<T, bool>> predicate)
    {
        return _set.AsNoTracking().Where(predicate);
    }

    public async Task<T?> GetById(Guid id, CancellationToken cancellationToken)
    {
        return await _set.FindAsync(id, cancellationToken);
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
            _set.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
}