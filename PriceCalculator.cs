using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PriceCalculatorKata.Interfaces;

namespace PriceCalculatorKata
{
    public class PriceCalculator : ICalculateTaxAndDiscount, IPrintInfo
    {
        private double taxPercentage { get; set; }
        private double discountPercentage { get; set; }
        private float tax { get; set; }
        private float uniDiscount { get; set; }
        private float upcDiscount { get; set; }
        private Product product { get;set; }
        public PriceCalculator(double taxPercentage,double discountPercentage,Product p)
        {
            this.taxPercentage = taxPercentage;
            this.discountPercentage = discountPercentage;   
            product=p;
            tax = CalculatePercentage(taxPercentage, p.price);
            uniDiscount=CalculatePercentage(discountPercentage, p.price);
        }
        public float CalculatePercentage(double percentage, double price)
        {
            return (float)Math.Round(price * percentage, 2);
        }
        public float CalculatePriceAfter(double percentage, double price)
        {
            return (float)(Math.Round(price * percentage, 2) + price);
        }
        public float TotalPriceAfter(float tax,float uniDiscount,double price,float discountUPC)
        {
            float priceAfter= (float)(Math.Round(price + tax - uniDiscount- discountUPC,2));
          
            return priceAfter;
        }
        public string PrintInfo(UPCRepo repo)
        {
            UPCDiscount upcD =null;
            foreach (UPCDiscount upc in repo.upcRepo)
            {
                if (upc.UPC == product.UPC)
                {
                    upcD= upc;
                    break;
                }
            }
            if (upcD != null)
            {
                upcDiscount = CalculatePercentage(upcD.discount, product.price);
            }
            return $"Sample product: Book with name = “{product.Name}”, UPC={product.UPC}, price=${product.price}, \n" +
                $"Tax={taxPercentage*100}%,universal discount={discountPercentage*100}%,UPC-Discount={upcD.discount*100}% for UPC={product.UPC} \n" +
                $"Tax Amount= {tax}, Universal Discount Amount={uniDiscount}, UPC Discount={upcDiscount} \n" +
                $"Price Before= ${product.price} Price After= ${TotalPriceAfter(tax,uniDiscount,product.price,upcDiscount)} \n" +
                $"Total Discount Amount={uniDiscount+upcDiscount}";
        }

        public string Report()
        {
            return $"Tax = {taxPercentage*100}%, discount = {discountPercentage*100}% \n" +
                $"Program prints price ${product.price} \n" +
                $"Program displays ${uniDiscount+upcDiscount} amount which was deduced";
        }

    }
}
