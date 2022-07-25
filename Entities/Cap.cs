using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceCalculatorKata.Entities
{
    public class Cap
    {
       public double amount { get; set; }
       public bool isPercent { get; set; }
        public Cap(double amount, bool isPercent)
        {
            this.amount = amount;
            this.isPercent = isPercent;
        }
    }
}
