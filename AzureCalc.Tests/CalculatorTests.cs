using Xunit;
using AzureCalc.Logic;

public class CalculatorTests
{
	private readonly Calculator _calculator;

	public CalculatorTests()
	{
		_calculator = new Calculator();
	}

	[Theory]
	[InlineData("Add", 2, 3, 5)]
	[InlineData("Sub", 5, 2, 3)]
	[InlineData("Multi", 4, 5, 20)]
	[InlineData("Div", 100, 2, 50)]
	public void BasicMath_ReturnsCorrectValue(string operation, double a, double b, double expected)
	{
		double result = operation switch
		{
			"Add"=> _calculator.Add(a, b),
			"Sub"=> _calculator.Sub(a, b),
			"Multi"=> _calculator.Multi(a, b),
			"Div"=> _calculator.Div(a, b),
			_ => throw new ArgumentException("Invalid Operation")
		};

		Assert.Equal(expected, result);
	}

	[Fact]
	public void Power_ReturnsCorrectValue()
	{
		double result = _calculator.Pow(2, 3);
		Assert.Equal(8, result);
	}

	[Fact]
	public void Log_ReturnsCorrectValue()
	{
		double result = _calculator.Log(2, 8);
		Assert.Equal(3, result);
	}
}
