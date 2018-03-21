using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class PlanningModel
    {
        public PlanningModel()
        {
            this.Images = new List<SampleImageModel>();
        }
        public string Name { get; set; }
        public List<SampleImageModel> Images { get; set; }
    }
}