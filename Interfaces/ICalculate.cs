using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceCalculatorKata.Interfaces
{
    public interface ICalculate
    {
        public double CalculatePercentage(double percentage, double price);
        public double TotalPriceAfter(bool beforOrAfter);
    }
}
