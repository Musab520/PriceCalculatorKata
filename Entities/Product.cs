using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceCalculatorKata
{
    public class Product
    {

        public string Name { get; set; }
        public int UPC { get; set; }
        public double price { get; set; }
        public Product(string name, int upc, double price)
        {
            Name = name;
            UPC = upc;
            this.price = price;
        }
        public override string ToString()
        {
            return "Name:"+Name+", UPC:"+UPC+",price:"+price;
        }
    }
    }

