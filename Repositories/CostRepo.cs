using PriceCalculatorKata.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceCalculatorKata.Repositories
{
    internal class CostRepo
    {
        public List<Cost> costRepo = new List<Cost>
        {
            new Cost("Transport Cost",2.2,12345,false),new Cost("Packaging Cost",0.01,12345,true)
        };
    }
}
