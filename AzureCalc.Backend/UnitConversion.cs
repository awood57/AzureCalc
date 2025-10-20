namespace AzureCalc.Backend
{
	public class UnitConversion
	{
		public double Convert(double value, string from, string to, string category)
		{
			if (category == "Temperature")
				return ConvertTemperature(value, from, to);
			
			var dict = category switch
			{
				"Distance" => UnitFactors.Distance,
        			"Mass" => UnitFactors.Mass,
        			"Volume" => UnitFactors.Volume,
        			"Temperature" => UnitFactors.Temperature,
        			"Time" => UnitFactors.Time,
        			"Speed" => UnitFactors.Speed,
        			"Energy" => UnitFactors.Energy,
        			_ => throw new ArgumentException("Unknown category")
			};

			return value * (dict[from] / dict[to]);
		}

		private double ConvertTemperature(double value, string from, string to)
		{
			double valueCelsius = from switch
			{
				"C" => value,
				"F" => (value - 32) * 5/9,
				"K" => value - 273.15,
				_ => throw new ArgumentException("Invalid unit")
			};

			return to switch
			{
				"C" => valueCelsius,
				"F" => valueCelsius * 9/5 + 32,
				"K" => valueCelsius + 273.15,
				_ => throw new ArgumentException("Invalid unit")
			};
		}
	}
}
