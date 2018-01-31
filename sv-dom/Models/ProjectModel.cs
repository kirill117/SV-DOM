using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class ProjectModel
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }

        public int Area { get; set; }
        public int RoomCount { get; set; }
        public int WcCount { get; set; }
        public int FloorsCount { get; set; }
        public string Size { get; set; }

        public int Index { get; set; }
        public bool ShowOnMain { get; set; }
    }
}