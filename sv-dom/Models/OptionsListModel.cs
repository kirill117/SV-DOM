using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    [Serializable]
    public class OptionsListModel
    {
        public List<OptionModel> Options { get; set; }
    }
}