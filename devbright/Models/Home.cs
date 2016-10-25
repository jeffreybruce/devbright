using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace devbright.Models
{
    public class Home
    {
        public int homeID { get; set; }
        public string title { get; set; }
        public decimal salePrice { get; set; }
        public string zipCode { get; set; }
        public decimal numberOfBedrooms { get; set; }
    }
}