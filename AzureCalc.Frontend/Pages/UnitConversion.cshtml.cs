using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using AzureCalc.Backend;
using AzureCalc.Backend.Services;

namespace AzureCalc.Frontend.Pages
{
	public class UnitConversionModel : PageModel
	{
		private readonly UnitConversion _unitConversion = new UnitConversion();
		// UnitConversionStorage goes here
		
		// Conversion variables
		[BindProperty]
		public double Num { get; set; }
		
		[BindProperty]
		public string Distance { get; set; } = string.Empty;

		public double? Result { get; set; }

		public void OnPost()
		{
			if(!string.IsNullOrEmpty(Distance))
			{
				switch(Distance)
				{
					case "ft":
						break;
					case "yards":
						break;
					case "km":
						break;
				}
			}
			// Other conversions go here
		}
	}
}
