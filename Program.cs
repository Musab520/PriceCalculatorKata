
using PriceCalculatorKata;

class Program
{
    public static void Main()
    {
        Product p1 = new Product("Gamestop", 12345, 20.25);
        PriceCalculator calc=new PriceCalculator(0.2,0.15,p1);
        Console.WriteLine(calc.PrintInfo());
        Console.WriteLine(calc.Report());
    }
}
