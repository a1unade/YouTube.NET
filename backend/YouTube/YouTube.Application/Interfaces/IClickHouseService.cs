using YouTube.Domain.ClickHouseEntity;

namespace YouTube.Application.Interfaces;

public interface IClickHouseService
{
    Task<View> IncrementView(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<T>> GetData<T>(CancellationToken cancellationToken) where T : new();
    
    Task<View?> GetByVideoId(Guid id, CancellationToken cancellationToken);
    Task AddData<T>(T entity, CancellationToken cancellationToken);
    
    Task DeleteData(string condition, CancellationToken cancellationToken);
    
    Task DeleteFromId(Guid id);
    
    Task DeleteTable(CancellationToken cancellationToken);
}