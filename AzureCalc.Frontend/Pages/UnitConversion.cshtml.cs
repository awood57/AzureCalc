using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using AzureCalc.Backend;
using AzureCalc.Backend.Services;

namespace AzureCalc.Frontend.Pages
{
	public class UnitConversionModel : PageModel
	{
		public class ConversionType
		{
			public string Name { get; set; }
			public List<string> Units { get; set; }
		}

		public List<ConversionType> Types { get; set; } = new List<ConversionType>
		{
			new ConversionType { Name = "Distance", Units = new List<string> { "ft", "yd", "km" } },
			new ConversionType { Name = "Mass", Units = new List<string> { "kg", "g", "lb" } },
        		new ConversionType { Name = "Volume", Units = new List<string> { "L", "mL", "gal" } },
        		new ConversionType { Name = "Temperature", Units = new List<string> { "°C", "°F", "K" } },
        		new ConversionType { Name = "Time", Units = new List<string> { "s", "min", "h" } },
        		new ConversionType { Name = "Speed", Units = new List<string> { "m/s", "km/h", "mph" } },
        		new ConversionType { Name = "Energy", Units = new List<string> { "J", "kJ", "cal" } },
		};
		private readonly UnitConversion _unitConversion = new UnitConversion();
		// UnitConversionStorage goes here
		
		// Conversion variables
		[BindProperty]
		public double Num { get; set; }
		
		[BindProperty]
		public string Unit{ get; set; }
		
		[BindProperty]
		public string SelectedUnit { get; set; }

		public double? Result { get; set; }

		public void OnPost()
		{
			// Other conversions go here
		}
	}
}
