using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Functions.Worker.Http;
using System.Net;
using System.Text.Json;
using AzureCalc.Logic;
using AzureCalc.Services;

namespace AzureCalc.Api;

public class CalculatorFunction
{
    private readonly ILogger<CalculatorFunction> _logger;
    private readonly Calculator _calculator;
    private readonly CalculationStorage _storage;

    public CalculatorFunction(ILogger<CalculatorFunction> logger, Calculator calculator, CalculationStorage storage)
    {
        _logger = logger;
        _calculator = calculator;
        _storage = storage;
    }

    // Basic arithmetic
    [Function("CalculatorFunctionBasic")]
    public async Task<HttpResponseData> CalculatorBasic(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "calculator/basic")]
        HttpRequestData request)
    {
        var query = System.Web.HttpUtility.ParseQueryString(request.Url.Query);

        // Parse the query and store variables, return error code if invalid parameters
        if (!double.TryParse(query["num1"], out var num1) ||
            !double.TryParse(query["num2"], out var num2) ||
            string.IsNullOrEmpty(query["operation"]))
        {
            return await BadRequest(request, new { error = "Invalid query parameters" });
        }

        var operation = query["operation"]!.ToLower();

        double? result = operation switch
        {
            "add" => _calculator.Add(num1, num2),
            "sub" => _calculator.Sub(num1, num2),
            "mul" => _calculator.Multi(num1, num2),
            "div" => _calculator.Div(num1, num2),
            _ => null
        };

        if (result is null)
            return await BadRequest(request, new { error = "Invalid Operation" });

        await _storage.SaveCalculationAsync(operation, num1, num2, result.Value);

        return await Ok(request, new { num1, num2, operation, result });
    }

    // Powers and logarithms
    [Function("CalculatorFunctionPower")]
    public async Task<HttpResponseData> CalculatorPower(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = "calculator/power")]
        HttpRequestData request)
    {
        var requestBody = await JsonSerializer.DeserializeAsync<PowerRequest>(request.Body);

        if (requestBody == null || string.IsNullOrEmpty(requestBody.Operation))
        {
            return await BadRequest(request, new { error = "Invalid request body" });
        }

        var op = requestBody.Operation.ToLower();

        double? result = op switch
        {
            "power" => _calculator.Pow(requestBody.BaseNum, requestBody.ExponentLog),
            "log" => _calculator.Log(requestBody.BaseNum, requestBody.ExponentLog),
            _ => null
        };

        if (result is null)
            return await BadRequest(request, new { error = "Invalid power operation" });

        await _storage.SaveCalculationAsync(op, requestBody.BaseNum, requestBody.ExponentLog, result.Value);

        return await Ok(request, new
        {
            requestBody.BaseNum,
            requestBody.ExponentLog,
            requestBody.Operation,
            result
        });
    }

    private async Task<HttpResponseData> Ok(HttpRequestData req, object body)
    {
        var resp = req.CreateResponse(HttpStatusCode.OK);
        await resp.WriteAsJsonAsync(body);
        return resp;
    }

    private async Task<HttpResponseData> BadRequest(HttpRequestData req, object body)
    {
        var resp = req.CreateResponse(HttpStatusCode.BadRequest);
        await resp.WriteAsJsonAsync(body);
        return resp;
    }

    // Request body model for power/log operations
    public class PowerRequest
    {
        public double BaseNum { get; set; }
        public double ExponentLog { get; set; }
        public string Operation { get; set; } = string.Empty;
    }
}

