namespace AzureCalc.Backend
{
	public class Calculator
	{
		// Basic math
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
		// Powers and Logarithms
		public double Pow(double a, double b)
		{
			return Math.Pow(a, b);
		}
		
		public double Log(double a, double b)
		{
			return Math.Log(b, a);
		}
	}
}
