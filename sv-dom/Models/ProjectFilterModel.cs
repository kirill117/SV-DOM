using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class ProjectFilterModel
    {
        public int Type { get; set; }
        public bool Area1 { get; set; }
        public bool Area2 { get; set; }
        public bool Area3 { get; set; }
        public bool Matherial1 { get; set; }
        public bool Matherial2 { get; set; }
        public bool Matherial3 { get; set; }
        public bool Price1 { get; set; }
        public bool Price2 { get; set; }
        public bool Price3 { get; set; }
        public bool Floor1 { get; set; }
        public bool Floor2 { get; set; }
        public bool Floor3 { get; set; }

        public bool IsEmpty()
        {
            return !Area1 && !Area2 && !Area3 && !Matherial1 && !Matherial2 && !Matherial3 && !Price1 && !Price2 && !Price3 && !Floor1 && !Floor2 && !Floor3;
        }

    }
}