
namespace PriceCalculatorKata
{
    using System.Collections.Generic;
    public class UPCRepo
    {
        public List<UPCDiscount> upcRepo = new List<UPCDiscount>
        {
            new UPCDiscount(0.07,12345,true),new UPCDiscount(0.5,12346,true),new UPCDiscount(0.6,12347,false)
        };
        


    }
}
