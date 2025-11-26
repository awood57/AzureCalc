public static class UnitFactors
{
	public static readonly Dictionary<string, double> Distance = new()
	{
		{ "m", 1.0 },
		{ "km", 1000.0 },
        	{ "cm", 0.01 },
        	{ "mm", 0.001 },
        	{ "ft", 0.3048 },
        	{ "yd", 0.9144 },
        	{ "mile", 1609.34 }
	};
	
	public static readonly Dictionary<string, double> Mass = new()
	{
		{ "kg", 1.0 },
		{ "g", 0.001 },
        	{ "lb", 0.453592 },
        	{ "oz", 0.0283495 }
    	};

	public static readonly Dictionary<string, double> Volume = new()
	{
		{ "L", 1.0},
		{ "mL", 0.001},
		{ "cL", 0.01},
		{ "dL", 0.1},
		{ "m^3", 1000},
		{ "gal", 3.78541},
		{ "qt", 0.946353},
		{ "pt", 0.473176},
		{ "cup", 0.24},
		{ "fl oz", 0.0295735}
	};

	// TODO: Offset Kelvin from Celsius
	public static readonly Dictionary<string, double> Temperature = new()
	{
		{ "C", 1.0},
		{ "K", 1.0},
		{ "F", 1.0}
	};
	
	public static readonly Dictionary<string, double> Time = new()
	{
		{ "s", 1.0},
		{ "min", 60.0},
		{ "h", 3600.0},
		{ "day", 86400.0},
		{ "week", 604800}
	};

	public static readonly Dictionary<string, double> Speed = new()
	{
		{ "m/s", 1.0 },
		{ "km/h", 0.277778},
		{ "mph", 0.44704},
		{ "ft/s", 0.3048},
		{ "knot", 0.514444}
	};

	public static readonly Dictionary<string, double> Energy = new()
	{
		{ "J", 1.0},
		{ "kJ", 1000},
		{ "cal", 4.184},
		{ "kcal", 4184},
		{ "Wh", 3600}
	};
}
