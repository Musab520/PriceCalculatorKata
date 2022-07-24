
namespace PriceCalculatorKata
{
    using System.Collections.Generic;
    public class UPCRepo
    {
        public List<UPCDiscount> upcRepo = new List<UPCDiscount>
        {
            new UPCDiscount(0.07f,12345),new UPCDiscount(0.5f,12346),new UPCDiscount(0.6f,12347)
        };
        


    }
}
