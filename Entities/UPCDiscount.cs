using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceCalculatorKata
{
    public class UPCDiscount
    {
        public double discount { get; set; }
        public int UPC { get; set; }
        public bool BeforeOrAfterTax = false;
        public UPCDiscount(double discount, int UPC,bool bfaft)
        {
            this.discount = discount;
            this.UPC = UPC;
            this.BeforeOrAfterTax = bfaft;
        }     
    }

    }
