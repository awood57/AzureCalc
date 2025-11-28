using Xunit;
using AzureCalc.Logic;

public class UnitConversionTests
{
	private readonly UnitConversion _converter;

	public UnitConversionTests()
	{
		_converter = new UnitConversion();
	}

	[Fact]
	public void DistanceConversion_ReturnsCorrectValue()
	{
		double result = _converter.Convert(2, "km", "m", "Distance");
		Assert.Equal(2000, result);
	}

	// Temperature conversion should be tested separately because it involves addition/subtraction
	[Fact]
	public void TemperatureConversion_ReturnsCorrectValue()
	{
		double result = _converter.Convert(30, "F", "K", "Temperature");
		Assert.Equal(272.0389, result, 4);
	}
}
