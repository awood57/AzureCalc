using AzureCalc.Services;
using AzureCalc.Logic;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllers();


var connectionString = builder.Configuration.GetConnectionString("AzureStorage");

if (string.IsNullOrEmpty(connectionString))
{
	throw new InvalidOperationException("Error: AzureStorage connection string not found.");
}

builder.Services.AddSingleton<CalculationStorage>(sp => new CalculationStorage(connectionString));
builder.Services.AddSingleton<ConversionStorage>(sp => new ConversionStorage(connectionString));

builder.Services.AddSingleton<Calculator>();
builder.Services.AddSingleton<UnitConversion>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();
app.MapControllers();

app.Run();
