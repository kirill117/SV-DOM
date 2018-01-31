using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using Helpers;

namespace sv_dom.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //var p = new ProjectsListModel();
            //p.Projects = new List<ProjectModel>();
            //p.Projects.Add(new ProjectModel() { Id=1 });
            //XMLHelper.Save(p);

            //var c = new PriceListModel();
            //c.Prices = new List<PriceModel>();
            //c.Prices.Add(new PriceModel() { ProjectID = 1 });
            //XMLHelper.Save(c);

            return View();
        }

        public ActionResult Contacts()
        {
            return View();
        }
        
        public ActionResult Price()
        {
            return View();
        }
        public ActionResult Services()
        {
            return View();
        }
        public ActionResult Gallery()
        {
            return View();
        }
        public ActionResult Catalog()
        {
            return View();
        }
        public ActionResult ProjectsList(int? type)
        {
            var model = MainHelper._projects.Projects;
            return View(model);
        }

    }
}