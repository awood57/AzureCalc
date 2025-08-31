using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using AzureCalc.Backend;
using AzureCalc.Backend.Services;

namespace AzureCalc.Frontend.Pages
{
	public class CalculatorModel : PageModel
	{
		private readonly Calculator _calculator = new Calculator();
		private readonly CalculationStorage _storage;

		public CalculatorModel(CalculationStorage storage)
		{
			_storage = storage;
		}

		[BindProperty]
		public double Num1 { get; set; }
	
		[BindProperty]
		public double Num2 { get; set; }
	
		[BindProperty]
		public string Operation { get; set; } = string.Empty;
	
		public double? Result { get; set; }
	
		public void OnPost()
		{
			switch(Operation)
			{
				case "add":
					Result = _calculator.Add(Num1, Num2);
					break;
				case "sub":
					Result = _calculator.Sub(Num1, Num2);
					break;
				case "mul":
					Result = _calculator.Multi(Num1, Num2);
					break;
				case "div":
					Result = _calculator.Div(Num1, Num2);
					break;
				default:
					Result = null;
					break;
			}
			if (Result.HasValue)
			{
				_storage.SaveCalculationAsync(Operation, Num1, Num2, Result.Value);
			}
		}
	}
}
