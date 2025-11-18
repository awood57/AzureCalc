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

public class ConversionStorage : TableStorage<ConversionEntity>
{
    public ConversionStorage(string connectionString)
        : base(connectionString, "Conversions") { }

    public async Task SaveConversionAsync(string fromUnit, string toUnit, double inputValue, double outputValue)
    {
        if (double.IsNaN(outputValue) || double.IsInfinity(outputValue))
            return;

        var entity = new ConversionEntity
        {
            FromUnit = fromUnit,
            ToUnit = toUnit,
            InputValue = inputValue,
            OutputValue = outputValue
        };

        await SaveAsync(entity);
    }
}

