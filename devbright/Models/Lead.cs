using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace devbright.Models
{
    public class Lead
    {
        public int leadID { get; set; }
        public string name { get; set; }
        public string emailAddress { get; set; }
        public string homeTitle { get; set; }
        public int homeID { get; set; }
    }
}