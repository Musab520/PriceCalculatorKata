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
        private Cap cap = new Cap(0.5,true);
        private double currPrice = 0;
        private double currTax = 0;
        private double currUniDiscount = 0;
        private double currUPCDiscount = 0;
        private Product product { get;set; }
        private bool AdditiveOrMultiplicative { get; set; }
        public PriceCalculator(double taxPercentage,double discountPercentage,Product p,bool add)
        {
            this.taxPercentage = taxPercentage;
            this.discountPercentage = discountPercentage;   
            product=p;
            AdditiveOrMultiplicative = add;
        }
        public double CalculatePercentage(double percentage, double price)
        {
            return Math.Round(price * percentage, 4);
        }
        public double TotalPriceAfter(bool beforeOrafter)
        {
            ClearValues();
            if (beforeOrafter)
            {
                currTax = CalculatePercentage(taxPercentage, product.price);
                currPrice = Math.Round(product.price+currTax,4);
                if (AdditiveOrMultiplicative) { AdditiveDiscounts(); } else { MultiplicativeDiscounts(); };
                IEnumerable<Cost> list = costRepo.costRepo.Select(cost => cost).Where(cost => cost.upc == product.UPC);
                foreach (Cost c in list)
                {
                   currPrice += c.isPercent ? (product.price * c.amount) : (c.amount);  
                }
            }
            else
            {
                MultiplicativeDiscounts();
                double capper = cap.isPercent ? (cap.amount * product.price) : cap.amount;
                currUniDiscount = CalculatePercentage(discountPercentage, currPrice);
                currPrice = currUniDiscount + currUPCDiscount <= capper ? Math.Round(currPrice - currUniDiscount,4) : Math.Round(product.price - capper,4);
                currTax = CalculatePercentage(taxPercentage, currPrice);
                currPrice = Math.Round(currPrice + currTax,4);
                IEnumerable<Cost> list = costRepo.costRepo.Select(cost => cost).Where(cost => cost.upc == product.UPC);
                foreach (Cost c in list)
                {
                    currPrice += c.isPercent ? Math.Round((product.price * c.amount),4) : (c.amount);
                }
            }
          
            return Math.Round(currPrice,2);
        }
        public void AdditiveDiscounts()
        {
            currUniDiscount = CalculatePercentage(discountPercentage, product.price);
            currUPCDiscount = CalculatePercentage(upcDiscountPercentage, product.price);
            double capper = cap.isPercent ? (cap.amount * product.price) : cap.amount;
            if (currUniDiscount + currUPCDiscount <= capper)
            {
                currPrice = Math.Round(currPrice - currUniDiscount,4);
                currPrice = Math.Round(currPrice - currUPCDiscount,4);
            }
            else
            {
                currPrice = Math.Round(currPrice - capper,4);
            }
        }

        public void MultiplicativeDiscounts()
        {
            currUPCDiscount = CalculatePercentage(upcDiscountPercentage, product.price);
            currPrice = product.price - currUPCDiscount;
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
            double total = TotalPriceAfter(beforeOrAfter);
            return Report(total);
        }

        public string Report(double total)
        {
            double capper = cap.isPercent ? (cap.amount * product.price) : cap.amount;
            double totalDiscounts= capper>=currUniDiscount+currUPCDiscount ? currUniDiscount + currUPCDiscount : capper;
            string report= $"Cost = ${Math.Round(product.price,2)} {product.currency} \n" +
                $"Tax = ${Math.Round(currTax,2)} {product.currency} \n" +
                $"Discounts = ${ Math.Round(totalDiscounts,2) } {product.currency} \n";
                IEnumerable<Cost> list = costRepo.costRepo.Select(cost => cost).Where(cost=>cost.upc==product.UPC);
                 foreach (Cost cost in list)
            {
                report += cost.description + ": " + (cost.isPercent ? Math.Round(cost.amount * product.price,2) : Math.Round(cost.amount,2)) + ""+ product.currency +"\n";
            }
                report += $"TOTAL = ${Math.Round(total, 4)} {product.currency} \n";
            return report;
        }

    }
}
