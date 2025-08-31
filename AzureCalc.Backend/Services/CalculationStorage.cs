using System;
using System.Threading.Tasks;
using Azure;
using Azure.Data.Tables;

namespace AzureCalc.Backend.Services;

public class CalculationEntity : ITableEntity
{
	public string PartitionKey { get; set; } = "Calculation";
	public string RowKey { get; set; } = Guid.NewGuid().ToString();
	public DateTimeOffset? Timestamp { get; set; }
	public ETag ETag { get; set; }

	public double A { get; set; }
	public double B { get; set; }
	public string Operation { get; set; } = string.Empty;
	public double Result { get; set; }
}

public class CalculationStorage
{
	private readonly TableClient _tableClient;

	public CalculationStorage(string connectionString)
	{
		var serviceClient = new TableServiceClient(connectionString);
		_tableClient = serviceClient.GetTableClient("Calculations");
		_tableClient.CreateIfNotExists();
	}

	public async Task SaveCalculationAsync(string operation, double a, double b, double result)
	{
		var entity = new CalculationEntity
        	{
            		A = a,
            		B = b,
            		Operation = operation,
            		Result = result
        	};

        await _tableClient.AddEntityAsync(entity);
    	}
}
