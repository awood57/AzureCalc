using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AzureCalc.Api;

public class UnitConversionFunction
{
    private readonly ILogger<UnitConversionFunction> _logger;

    public UnitConversionFunction(ILogger<UnitConversionFunction> logger)
    {
        _logger = logger;
    }

    [Function("UnitConversionFunction")]
    public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");
        return new OkObjectResult("Welcome to Azure Functions!");
    }
}
