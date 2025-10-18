using System.Globalization;

namespace Ch03;

public enum Bmi { Underweight, Healthy, Overweight }

// Write a console app that calculates a user’s Body Mass Index (BMI):
// 1. Prompt the user for their height in meters and weight in kilograms.
// 2. Calculate the BMI as weight / height2.
// 3. Output a message: underweight (BMI < 18.5), overweight (BMI >= 25), or healthy.
// 4. Structure your code so that pure and impure parts are separate.
// 5. Unit test the pure parts.
// 6. Unit test the overall workflow using the function-based approach to abstract 
//    away the reading from and writing to the console.
public static class PureFunctions
{
    public static Bmi CalculateBmi(double weight, double height)
    {
        var bmi = Math.Round(weight / Math.Pow(height, 2), 2);
        return bmi switch
        {
            < 18.5 => Bmi.Underweight,
            >= 25 => Bmi.Overweight,
            _ => Bmi.Healthy
        };
    }

    public static void Main()
    {
        var weight = Read("weight");
        var height = Read("height");
        Console.WriteLine($"Your BMI is: {CalculateBmi(weight, height)}");
    }

    private static double Read(string field)
    {
        Console.Write($"Please enter your {field}: ");
        return double.Parse(Console.ReadLine()!, CultureInfo.InvariantCulture);
    }
}