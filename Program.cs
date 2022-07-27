
using PriceCalculatorKata;

class Program
{
    public static void Main()
    {
        Product p1 = new Product("Game", 12345, 20.25,"GDP");
        PriceCalculator calc=new PriceCalculator(0.21,0.15,p1,true);
        UPCRepo repo=new UPCRepo();
        Console.WriteLine(calc.PrintInfo());
    }
}
