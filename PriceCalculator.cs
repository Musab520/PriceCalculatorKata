using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PriceCalculatorKata.Interfaces;

namespace PriceCalculatorKata
{
    public class PriceCalculator : ICalculateTaxAndDiscount,IPrintInfo
    {
        private double taxPercentage { get; set; }
        private double discountPercentage { get; set; }
        private float tax { get; set; }
        private float discount { get; set; }
        private Product product { get; set; }
        public PriceCalculator(double taxPercentage,double discountPercentage,Product p)
        {
            this.taxPercentage = taxPercentage;
            this.discountPercentage = discountPercentage;   
            product=p;
            tax = CalculatePercentage(taxPercentage, p.price);
            discount=CalculatePercentage(discountPercentage, p.price);
        }
        public float CalculatePercentage(double percentage, double price)
        {
            return (float)Math.Round(price * percentage, 2);
        }
        public float CalculatePriceAfter(double percentage, double price)
        {
            return (float)(Math.Round(price * percentage, 2) + price);
        }
        public float TotalPriceAfter(float tax,float discount,double price)
        {
            return (float)(Math.Round(price + tax - discount,2));
        }
        public string PrintInfo()
        {
            return $"Sample product: Book with name = “{product.Name}”, UPC={product.UPC}, price=${product.price}, \n" +
                $"Tax={taxPercentage},discount={discount}, Tax Amount= {tax}, Discount Amount={discount}, \n" +
                $"Price Before= ${product.price} Price After= ${TotalPriceAfter(tax,discount,product.price)}";
        }

        public string Report()
        {
            return $"Tax = {taxPercentage*100}%, discount = {discountPercentage*100}% \n" +
                $"Program prints price ${product.price} \n" +
                $"Program displays ${discount} amount which was deduced";
        }

    }
}
