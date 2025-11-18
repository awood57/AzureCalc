using System;
using System.Threading.Tasks;
using Azure;
using Azure.Data.Tables;

public abstract class TableStorage<T> where T : class, ITableEntity, new()
{
	protected readonly TableClient _tableClient;

	protected TableStorage(string connectionString, string tableName)
	{
		var serviceClient = new TableServiceClient(connectionString);
		_tableClient = serviceClient.GetTableClient(tableName);
		_tableClient.CreateIfNotExists();
	}

	public async Task SaveAsync(T entity)
	{
		await _tableClient.AddEntityAsync(entity);
	}

	public async Task<List<T>> GetAllAsync()
	{
		var query = _tableClient.QueryAsync<T>();
		var results = new List<T>();

		await foreach (var entity in query)
			results.Add(entity);

		return results;
	}
}
