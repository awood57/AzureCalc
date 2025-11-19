using Microsoft.AspNetCore.Mvc;
using AzureCalc.Backend;
using AzureCalc.Backend.Services;

namespace AzureCalc.Frontend.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class UnitConversionApiController : ControllerBase
	{
		private readonly UnitConversion _converter;
		private readonly ConversionStorage _storage;
		// TODO: Unit conversion storage??

		public UnitConversionApiController(ConversionStorage storage)
		{
			_converter = new UnitConversion();
			_storage = storage;
		}
		
		// Return categories for UI
		[HttpGet("categories")]
		public IActionResult GetCategories()
		{
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

			return Ok(categories);
		}

		// Conversion endpoint
		[HttpGet("convert")]
		public async Task<IActionResult> ConvertUnit(double value, string from, string to, string category)
		{
			double result = _converter.Convert(value, from, to, category);
			await _storage.SaveConversionAsync(from, to, value, result);
			return Ok(new { value, from, to, result });

		}

	}
}
