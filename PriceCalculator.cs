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
        private double upcDiscountPercentage { get; set; }
        private float currPrice = 0;
        private float currTax = 0;
        private float currUniDiscount = 0;
        private float currUPCDiscount = 0;
        private Product product { get;set; }
        public PriceCalculator(double taxPercentage,double discountPercentage,Product p)
        {
            this.taxPercentage = taxPercentage;
            this.discountPercentage = discountPercentage;   
            product=p;
        }
        public float CalculatePercentage(double percentage, double price)
        {
            return (float)Math.Round(price * percentage, 2);
        }
        public float TotalPriceAfter(bool beforeOrafter)
        {
            ClearValues();
            if (beforeOrafter)
            {
                currTax = CalculatePercentage(taxPercentage, product.price);
                currUniDiscount = CalculatePercentage(discountPercentage, product.price);
                currUPCDiscount=CalculatePercentage(upcDiscountPercentage, product.price);
                currPrice = currTax;
                currPrice = currPrice - currUniDiscount;
                currPrice = currPrice - currUPCDiscount;
            }
            else
            {
                currUPCDiscount = CalculatePercentage(upcDiscountPercentage, product.price);
                currPrice = (float)product.price - CalculatePercentage(upcDiscountPercentage,product.price);
                currTax = CalculatePercentage(taxPercentage, currPrice);
                currUniDiscount = CalculatePercentage(discountPercentage, currPrice);
                currPrice = currPrice + currTax;
                currPrice = currPrice - currUniDiscount;
            }
          
            return (float)Math.Round(currPrice,2);
        }
        public void ClearValues()
        {
            currPrice = 0;
            currUPCDiscount = 0;
            currTax = 0;
            currUniDiscount = 0;
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
            bool beforeOrAfter = false;
            if (upcD != null)
            {
                upcDiscountPercentage = upcD.discount;
                beforeOrAfter = upcD.BeforeOrAfterTax;
            }
            else
            {
                upcDiscountPercentage = 0;
            }
            float total = TotalPriceAfter(beforeOrAfter);
            return $"Sample product: Book with name = “{product.Name}”, UPC={product.UPC}, price=${product.price}, \n" +
                $"Tax={taxPercentage*100}%,universal discount={discountPercentage*100}%,UPC-Discount={Math.Round(upcDiscountPercentage*100,2)}% for UPC={product.UPC} \n" +
                $"Tax Amount= {currTax}$, Universal Discount Amount={currUniDiscount}$, UPC Discount={currUPCDiscount}$ \n" +
                $"Price Before= ${product.price} Price After= ${total} \n" +
                $"Total Discount Amount={currUniDiscount+currUPCDiscount}$";
        }

        public string Report()
        {
            return $"Tax = {taxPercentage*100}%, discount = {discountPercentage*100}% \n" +
                $"Program prints price ${product.price} \n" +
                $"Program displays ${currUniDiscount+currUPCDiscount} amount which was deduced";
        }

    }
}
