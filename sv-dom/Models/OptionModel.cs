using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Models
{
    public class OptionModel
    {
        [XmlAttribute()]
        public Guid ID { get; set; }
        [XmlAttribute()]
        public int ProjectID { get; set; }
        [XmlAttribute()]
        public int MatherialID { get; set; }
        [XmlAttribute()]
        public int ConfigurationID { get; set; }
        [XmlAttribute()]
        public decimal Cost { get; set; }

        public string Name { get; set; }
    }
}