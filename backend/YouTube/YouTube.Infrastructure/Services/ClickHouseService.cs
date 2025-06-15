using System.Data;
using System.Reflection;
using ClickHouse.Client.ADO;
using ClickHouse.Client.ADO.Parameters;
using ClickHouse.Client.Copy;
using Microsoft.Extensions.Configuration;
using YouTube.Application.Interfaces;
using YouTube.Domain.ClickHouseEntity;

namespace YouTube.Infrastructure.Services;

public class ClickHouseService : IClickHouseService, IDisposable, IAsyncDisposable
{
    private readonly ClickHouseConnection _clickHouseConnection;
    private const string TableName = nameof(View);

    public ClickHouseService(IConfiguration configuration)
    {
        _clickHouseConnection= new ClickHouseConnection(configuration["ConnectionStrings:ClickHouse"]);
        _clickHouseConnection.Open();
    }

    public async Task<View> IncrementView(Guid videoId, CancellationToken cancellationToken)
    {
        var view = await GetByVideoId(videoId, cancellationToken);
        
        if (view == null)
        {
            throw new InvalidOperationException($"View with VideoId {videoId} not found.");
        }
        
        var deleteCmd = _clickHouseConnection.CreateCommand();
        deleteCmd.CommandText = $"ALTER TABLE {TableName} DELETE WHERE VideoId = '{videoId}'";
        await deleteCmd.ExecuteNonQueryAsync(cancellationToken);
        
        view.ViewCount += 1;
        await AddData(view, cancellationToken);

        return view;
    }
    
    public async Task<View?> GetByVideoId(Guid id, CancellationToken cancellationToken)
    {
        await using var cmd = _clickHouseConnection.CreateCommand();
        cmd.CommandText = $"SELECT * FROM {TableName} WHERE VideoId = '{id}' LIMIT 1";

        View? view = null;

        await using var reader = await cmd.ExecuteReaderAsync(cancellationToken);
        if (await reader.ReadAsync(cancellationToken))
        {
            view = new View
            {
                Id = (Guid)reader["Id"],
                VideoName = reader["VideoName"].ToString()!,
                VideoId = (Guid)reader["VideoId"],
                ChannelId = (Guid)reader["ChannelId"],
                ViewCount = (long)reader["ViewCount"]
            };
        }
        
        if (view == null)
        {
            throw new InvalidOperationException($"View with VideoId {id} not found.");
        }

        return view;
    }

    public async Task<IEnumerable<T>> GetData<T>(CancellationToken cancellationToken) where T : new()
    {
        await using var cmd = _clickHouseConnection.CreateCommand();
        cmd.CommandText = $"SELECT * FROM {TableName}";
        
        await using var reader = await cmd.ExecuteReaderAsync(cancellationToken);
        
        var result = new List<T>();
        var properties = typeof(T).GetProperties();
        
        while (await reader.ReadAsync(cancellationToken))
        {
            var item = new T();
            for (var i = 0; i < reader.FieldCount; i++)
            {
                var prop = properties.FirstOrDefault(p => 
                    string.Equals(p.Name, reader.GetName(i), StringComparison.CurrentCultureIgnoreCase));
                
                if (prop != null && reader[i] != DBNull.Value)
                {
                    prop.SetValue(item, reader[i]);
                }
            }
            
            result.Add(item);
        }
        
        return result;
    }
    
   

    public async Task AddData<T>(T entity, CancellationToken cancellationToken)
    {
        var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

        var dataTable = new DataTable();
        foreach (var prop in properties)
        {
            var type = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
            dataTable.Columns.Add(prop.Name, type);
        }

        var row = dataTable.NewRow();
        foreach (var prop in properties)
        {
            var value = prop.GetValue(entity) ?? DBNull.Value;
            row[prop.Name] = value;
        }

        dataTable.Rows.Add(row);

        using var bulkCopy = new ClickHouseBulkCopy(_clickHouseConnection)
        {
            DestinationTableName = TableName
        };

        await bulkCopy.InitAsync();

        await bulkCopy.WriteToServerAsync(dataTable, cancellationToken);
    }

    public async Task DeleteData(string condition, CancellationToken cancellationToken)
    {
        await using var cmd = _clickHouseConnection.CreateCommand();
        cmd.CommandText = $"ALTER TABLE {TableName} DELETE WHERE {condition}";
        await cmd.ExecuteNonQueryAsync(cancellationToken);
    }

    public async Task DeleteFromId(Guid id)
    {
        var query = $"ALTER TABLE {TableName} DELETE WHERE id = {id}";

        await using var cmd = _clickHouseConnection.CreateCommand();
        cmd.CommandText = query;
        await cmd.ExecuteNonQueryAsync();
    }

    public async Task DeleteTable(CancellationToken cancellationToken)
    {
        await using var cmd = _clickHouseConnection.CreateCommand();
        cmd.CommandText = $"DROP TABLE IF EXISTS {TableName}";
        await cmd.ExecuteNonQueryAsync(cancellationToken);
    }

    public void Dispose()
    {
        ((IDisposable)_clickHouseConnection).Dispose();
    }

    public async ValueTask DisposeAsync()
    {
        await _clickHouseConnection.DisposeAsync();
    }
}