using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AzureCalc.Logic;
using AzureCalc.Services;

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();

builder.Services
    .AddApplicationInsightsTelemetryWorkerService()
    .ConfigureFunctionsApplicationInsights();

var connectionString = "UseDevelopmentStorage=true"; // or from configuration

builder.Services.AddSingleton<Calculator>();
builder.Services.AddSingleton<UnitConversion>();
builder.Services.AddSingleton<CalculationStorage>(_ => new CalculationStorage(connectionString));
builder.Services.AddSingleton<ConversionStorage>(_ => new ConversionStorage(connectionString));

builder.Build().Run();
