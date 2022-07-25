using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceCalculatorKata
{
    public class Product
    {

        private string Name { get; set; }
        private int UPC { get; set; }
        private double price { get; set; }
        private Double percentage { get; set; }
        private float tax { get; set; }
        public Product(string name, int upc, double price, double percentage)
        {
            Name = name;
            UPC = upc;
            this.price = price;
            this.percentage = percentage;
            tax = this.CalculateTax();
        }

        private float CalculateTax()
        {
            return (float)Math.Round(price * percentage + price, 2);
        }
        public override string ToString()
        {
            return $"Sample product: Book with name = “{Name}”, UPC={UPC}, price=${price}, \nProduct price reported as { price} before tax and {tax} after { percentage*100} % tax.";
        }
    }
    }

