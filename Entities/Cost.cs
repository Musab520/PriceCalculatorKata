using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceCalculatorKata.Entities
{
    public class Cost
    {
        public string description { get; set; }
        public double amount { get; set; }
        public bool isPercent { get; set; }
        public int upc { get; set; }
        public Cost(string des,double amount,int upc, bool per)
        {
            description = des;
            this.amount = amount;
            isPercent = per;
            this.upc = upc;
        }

    }
}
