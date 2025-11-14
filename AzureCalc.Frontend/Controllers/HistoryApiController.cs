using Microsoft.AspNetCore.Mvc;
using AzureCalc.Backend.Services;

namespace AzureCalc.Frontend.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class HistoryApiController : ControllerBase
	{
		private readonly CalculationStorage _storage;

		public HistoryApiController(CalculationStorage storage)
		{
			_storage = storage;
		}

		[HttpGet("all")]
		public async Task<IActionResult> GetAll()
		{
			var entries = await _storage.GetAllAsync();
			return Ok(entries);
		}
	}
}
