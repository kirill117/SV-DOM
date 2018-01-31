using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    [Serializable]
    public class PriceListModel
    {
        public List<PriceModel> Prices { get; set; }
    }
}