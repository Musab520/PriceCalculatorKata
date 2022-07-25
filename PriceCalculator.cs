using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PriceCalculatorKata.Entities;
using PriceCalculatorKata.Interfaces;
using PriceCalculatorKata.Repositories;

namespace PriceCalculatorKata
{
    public class PriceCalculator : ICalculate, IPrintInfo
    {
        private double taxPercentage { get; set; }
        private double discountPercentage { get; set; }
        private double upcDiscountPercentage { get; set; }
        private UPCRepo upcRepo=new UPCRepo();
        private CostRepo costRepo = new CostRepo();
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
                currPrice =(float) product.price+currTax;
                currPrice = currPrice - currUniDiscount;
                currPrice = currPrice - currUPCDiscount;
                IEnumerable<Cost> list = costRepo.costRepo.Select(cost => cost).Where(cost => cost.upc == product.UPC);
                foreach (Cost c in list)
                {
                   currPrice += c.isPercent ? (float)(product.price * c.amount) : (float)(c.amount);  
                }
            }
            else
            {
                currUPCDiscount = CalculatePercentage(upcDiscountPercentage, product.price);
                currPrice = (float)product.price - CalculatePercentage(upcDiscountPercentage,product.price);
                currTax = CalculatePercentage(taxPercentage, currPrice);
                currUniDiscount = CalculatePercentage(discountPercentage, currPrice);
                currPrice = currPrice + currTax;
                currPrice = currPrice - currUniDiscount;
                IEnumerable<Cost> list = costRepo.costRepo.Select(cost => cost).Where(cost => cost.upc == product.UPC);
                foreach (Cost c in list)
                {
                    currPrice += c.isPercent ? (float)(product.price * c.amount) : (float)(c.amount);
                }
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
        public string PrintInfo()
        {
            UPCDiscount? upcD =null;
            foreach (UPCDiscount upc in upcRepo.upcRepo)
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
            return Report(total);
        }

        public string Report(float total)
        {
            string report= $"Cost = ${product.price} \n" +
                $"Tax = ${currTax} \n" +
                $"Discounts = ${currUPCDiscount + currUniDiscount} \n";
                IEnumerable<Cost> list = costRepo.costRepo.Select(cost => cost).Where(cost=>cost.upc==product.UPC);
                 foreach (Cost cost in list)
            {
                report += cost.description + ": " + (cost.isPercent ? Math.Round(cost.amount * product.price,2) : Math.Round(cost.amount,2)) + " \n";
            }
                report += $"TOTAL = ${total} \n";
            return report;
        }

    }
}
