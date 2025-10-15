using Microsoft.AspNetCore.Mvc;
using AzureCalc.Backend;
using AzureCalc.Backend.Services;

namespace AzureCalc.Frontend.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class CalculatorApiController : ControllerBase
	{
		private readonly Calculator _calculator;
		private readonly CalculationStorage _storage;

		public CalculatorApiController(CalculationStorage storage)
		{
			_calculator = new Calculator();
			_storage = storage;
		}
		[HttpGet("basic")]
		public IActionResult Basic([FromQuery] double num1, [FromQuery] double num2, [FromQuery] string operation)
		{
			operation = operation.ToLower();

			double? result = operation switch
			{
				"add" => _calculator.Add(num1, num2),
				"sub" => _calculator.Sub(num1, num2),
				"mul" => _calculator.Multi(num1, num2),
				"div" => _calculator.Div(num1, num2),
				_ => null
			};
			
			if (result == null)
				return BadRequest(new { error = "Invalid operation" });

			_storage.SaveCalculationAsync(operation, num1, num2, result.Value);
			return Ok(new { num1, num2, operation, result });
		}
		[HttpPost("power")]
		public IActionResult Power([FromBody] PowerRequest request)
		{
			var op = request.Operation.ToLower();

			double? result = op switch
			{
				"power" => _calculator.Pow(request.BaseNum, request.ExponentLog),
				"log" => _calculator.Log(request.BaseNum, request.ExponentLog),
				_ => null
			};

			if (result == null)
				return BadRequest(new { error = "Invalid power operation" });
			
			// Save power operation goes here
			//
			//
			return Ok(new { request.BaseNum, request.ExponentLog, request.Operation, result });
		}

		public class PowerRequest
		{
			public double BaseNum { get; set; }
			public double ExponentLog { get; set; }
			public string Operation { get; set; } = string.Empty;
		}
	}
}
