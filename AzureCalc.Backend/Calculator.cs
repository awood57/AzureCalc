namespace AzureCalc.Backend
{
	public class Calculator
	{
		public double Add(double a, double b)
		{
			return a + b;
		}

		public double Sub(double a, double b)
		{
			return a - b;
		}

		public double Multi(double a, double b)
		{
			return a * b;
		}

		public double Div(double a, double b)
		{
			if (b == 0)
				throw new DivideByZeroException("Divided by zero!");

			return a / b;
		}
	}
}
