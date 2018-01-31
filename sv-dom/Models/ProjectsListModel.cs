using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    [Serializable]
    public class ProjectsListModel
    {
        public List<ProjectModel> Projects { get; set; }
    }
}