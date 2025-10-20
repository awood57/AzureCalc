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
		// TODO: Unit conversion storage??

		public UnitConversionApiController()
		{
			_converter = new UnitConversion();
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
		public IActionResult ConvertUnity(double value, string from, string to, string category)
		{
			double result = _converter.Convert(value, from, to, category);
			return Ok(new { value, from, to, result });
		}

	}
}
