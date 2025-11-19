using Microsoft.AspNetCore.Mvc;
using AzureCalc.Backend.Services;

namespace AzureCalc.Frontend.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class HistoryApiController : ControllerBase
	{
		private readonly CalculationStorage _calcStorage;
		private readonly ConversionStorage _convStorage;

		public HistoryApiController(CalculationStorage calcStorage, ConversionStorage convStorage)
		{
			_calcStorage = calcStorage;
			_convStorage = convStorage;
		}

		[HttpGet("all")]
		public async Task<IActionResult> GetAll()
		{
			var calcTask = _calcStorage.GetAllAsync();
			var convTask = _convStorage.GetAllAsync();

			await Task.WhenAll(calcTask, convTask);

			var response = new
			{
				calculations = await calcTask,
				conversions = await convTask
			};

			return Ok(response);
		}
	}
}
