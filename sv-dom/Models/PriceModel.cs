using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class PriceModel
    {
        public int ProjectID { get; set; }
        public int MatherialID { get; set; }
        public int ConfigurationID { get; set; }
        public decimal Cost { get; set; }
    }
}