using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Varuosad.Models
{
    public class Part
    {
        public string SerialNumber { get; set; }
        public string Product_Decription { get; set; }
        public string Car { get; set; }
        public double Price { get; set; }
        public double Vat_Included { get; set; }
    }
}