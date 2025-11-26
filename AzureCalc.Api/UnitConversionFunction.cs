using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;
using AzureCalc.Logic;
using AzureCalc.Services;

namespace AzureCalc.Api;

public class UnitConversionFunction
{
    private readonly ILogger<UnitConversionFunction> _logger;
    private readonly UnitConversion _converter;
    private readonly ConversionStorage _storage;

    public UnitConversionFunction(
        ILogger<UnitConversionFunction> logger,
        UnitConversion converter,
        ConversionStorage storage)
    {
        _logger = logger;
        _converter = converter;
        _storage = storage;
    }

    // Return unit categories for the UI
    [Function("ConverterCategories")]
    public async Task<HttpResponseData> GetCategories(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "converter/categories")]
        HttpRequestData request)
    {
        _logger.LogInformation("Returning unit categories.");

        var categories = new Dictionary<string, List<string>>
        {
            { "Distance", UnitFactors.Distance.Keys.ToList() },
            { "Mass", UnitFactors.Mass.Keys.ToList() },
            { "Volume", UnitFactors.Volume.Keys.ToList() },
            { "Temperature", UnitFactors.Temperature.Keys.ToList() },
            { "Time", UnitFactors.Time.Keys.ToList() },
            { "Speed", UnitFactors.Speed.Keys.ToList() },
            { "Energy", UnitFactors.Energy.Keys.ToList() }
        };

        var response = request.CreateResponse(HttpStatusCode.OK);
        await response.WriteAsJsonAsync(categories);
        return response;
    }

    // Conversion endpoint
    [Function("ConverterFunction")]
    public async Task<HttpResponseData> ConvertUnit(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "converter/convert")]
        HttpRequestData request)
    {
        _logger.LogInformation("Processing unit conversion request.");

        var query = System.Web.HttpUtility.ParseQueryString(request.Url.Query);

        if (!double.TryParse(query["value"], out var value) ||
            string.IsNullOrEmpty(query["from"]) ||
            string.IsNullOrEmpty(query["to"]) ||
            string.IsNullOrEmpty(query["category"]))
        {
            var bad = request.CreateResponse(HttpStatusCode.BadRequest);
            await bad.WriteAsJsonAsync(new { error = "Invalid query parameters." });
            return bad;
        }

        string from = query["from"]!;
        string to = query["to"]!;
        string category = query["category"]!;

        double result;

        try
        {
            result = _converter.Convert(value, from, to, category);
        }
        catch (Exception ex)
        {
            var bad = request.CreateResponse(HttpStatusCode.BadRequest);
            await bad.WriteAsJsonAsync(new { error = ex.Message });
            return bad;
        }

        // Store the conversion
        await _storage.SaveConversionAsync(from, to, value, result);

        var response = request.CreateResponse(HttpStatusCode.OK);
        await response.WriteAsJsonAsync(new { value, from, to, category, result });

        return response;
    }
}

