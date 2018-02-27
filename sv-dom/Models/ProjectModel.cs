using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Models
{
    public class ProjectModel
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public Matherial[] Matherials { get; set; }


        public int Area { get; set; }
        public int RoomCount { get; set; }
        public int WcCount { get; set; }
        public int FloorsCount { get; set; }
        public string Size { get; set; }

        public int Index { get; set; }
        public bool ShowOnMain { get; set; }

        public string Description { get; set; }
    }

    [Serializable]
    public enum Matherial
    {
        [XmlEnum("1")]
        ОЦБ = 1,
        [XmlEnum("2")]
        КБ = 2,
        [XmlEnum("3")]
        ПБ = 3,
        [XmlEnum("4")]
        КОМБИ = 4
    }
}