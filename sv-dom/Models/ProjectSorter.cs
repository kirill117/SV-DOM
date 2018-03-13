using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class ProjectSorter : IComparer<ProjectModel>
    {
        public int _type;
        public ProjectSorter(int type)
        {
            _type = type;
        }
        public int Compare(ProjectModel p1, ProjectModel p2)
        {

            if (_type == 2)
            {
                if(new int[] { 37, 46, 48 }.Contains(p1.Id) && new int[] { 37, 46, 48 }.Contains(p2.Id) == false)
                {
                    return -1;
                }
            }
            else
            {
                return 1;
            }

            return 0;
        }
    }
}