using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceCalculatorKata.Interfaces
{
    public interface ICalculateTaxAndDiscount
    {
        public float CalculatePercentage(double percentage, double price);
        public float TotalPriceAfter(bool beforOrAfter);
    }
}
