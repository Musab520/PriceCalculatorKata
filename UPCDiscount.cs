using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceCalculatorKata
{
    public class UPCDiscount
    {
        public float discount { get; set; }
        public int UPC { get; set; }
        public UPCDiscount(float discount, int UPC)
        {
            this.discount = discount;
            this.UPC = UPC;
        }     
    }

    }
