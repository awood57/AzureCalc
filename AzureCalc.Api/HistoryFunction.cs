using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Functions.Worker.Http;
using System.Net;
using System.Text.Json;
using AzureCalc.Logic;
using AzureCalc.Services;

namespace AzureCalc.Api;

public class HistoryFunction
{
    private readonly ILogger<HistoryFunction> _logger;
    private readonly CalculationStorage _calcStorage;
    private readonly ConversionStorage _convStorage;

    public HistoryFunction(ILogger<HistoryFunction> logger, CalculationStorage calcStorage, ConversionStorage convStorage)
    {
        _logger = logger;
	_calcStorage = calcStorage;
	_convStorage = convStorage;
    }

    // Return all calculations and conversions from storage
    [Function("HistoryFunction")]
    public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = "history/all")] HttpRequestData request)
    {
        _logger.LogInformation("Fetching full history of calculations and conversions.");

	// Fetch histories
	var calcTask = _calcStorage.GetAllAsync();
	var convTask = _convStorage.GetAllAsync();
	await Task.WhenAll(calcTask, convTask);

	var responseObj = new
	{
		calculations = await calcTask,
		conversions = await convTask
	};

	// Create response and write json
	var response = request.CreateResponse(HttpStatusCode.OK);
	await response.WriteAsJsonAsync(responseObj);

	return response;
    }
}
