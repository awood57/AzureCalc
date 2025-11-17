using System;
using System.Threading.Tasks;
using Azure;
using Azure.Data.Tables;

namespace AzureCalc.Backend.Services;

public class ConversionEntity : ITableEntity
{
	public string PartitionKey { get; set; } = "Conversion";
	public string RowKey { get; set; } = Guid.NewGuid().ToString();
	public DateTimeOffset? Timestamp { get; set; }
	public ETag ETag { get; set; }

	public string FromUnit { get; set; } = string.Empty;
	public string ToUnit { get; set; } = string.Empty;
	public double InputValue { get; set; }
	public double OutputValue { get; set; }
}

public class ConversionStorage
{
	private readonly TableClient _tableClient;

	public ConversionStorage(string connectionString)
	{
		var serviceClient = new TableServiceClient(connectionString);
		_tableClient = serviceClient.GetTableClient("Conversions");
		_tableClient.CreateIfNotExists();
	}

	public async Task SaveConversionAsync(string fromUnit, string toUnit, double inputValue, double outputValue)
	{
		// Sanitize entry
		if (double.IsNaN(outputValue) || double.IsInfinity(outputValue))
		{
			return;
		}
	
		var entity = new ConversionEntity
		{
			FromUnit = fromUnit,
			ToUnit = toUnit,
			InputValue = inputValue,
			OutputValue = outputValue
		};

		await _tableClient.AddEntityAsync(entity);
	}

	public async Task<List<ConversionEntity>> GetAllAsync()
	{
		var query = _tableClient.QueryAsync<ConversionEntity>();
		var results = new List<ConversionEntity>();

		await foreach (var item in query)
			results.Add(item);

		return results;
	}
}
