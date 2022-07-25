
using PriceCalculatorKata;

class Program
{
    public static void Main()
    {
        Product p1 = new Product("Gamestop", 12345, 20.25);
        PriceCalculator calc=new PriceCalculator(0.2,0.15,p1);
        UPCRepo repo=new UPCRepo();
        Console.WriteLine(calc.PrintInfo(repo));
        Console.WriteLine(calc.Report());
    }
}
