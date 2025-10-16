using Ch03;

namespace Ch03.Tests;

public class BmiCalculatorTests
{
    [Theory]
    [InlineData(81.6466, 1.651, Ch03.Bmi.Overweight)]
    [InlineData(50.0, 1.7, Ch03.Bmi.Underweight)]
    [InlineData(60.0, 1.7, Ch03.Bmi.Healthy)]
    public void Bmi(double weight, double height, Bmi expected)
    {
        Bmi bmi = BmiCalculator.CalculateBmi(weight, height);
        Assert.Equal(expected, bmi);
    }
}