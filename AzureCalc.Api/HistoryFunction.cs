using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AzureCalc.Api;

public class HistoryFunction
{
    private readonly ILogger<HistoryFunction> _logger;

    public HistoryFunction(ILogger<HistoryFunction> logger)
    {
        _logger = logger;
    }

    [Function("HistoryFunction")]
    public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");
        return new OkObjectResult("Welcome to Azure Functions!");
    }
}
