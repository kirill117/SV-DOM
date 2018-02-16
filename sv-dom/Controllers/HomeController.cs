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

            var filter = new ProjectFilterModel();
            if (type.HasValue)
            {
                model = model.Where(s => s.Type.Split(new[] { ';' }).Any(x => x == type.ToString())).ToList();
                switch(type.Value)
                {
                    case 1:
                        filter.Matherial1 = true;
                        break;
                    case 2:
                        filter.Matherial2 = true;
                        break;
                    case 3:
                        filter.Matherial3 = true;
                        break;
                }
            }

            ViewBag.ProjectFilter = filter;
            return View(model);
        }

        public ActionResult Project(int id)
        {
            var model = MainHelper._projects.Projects.FirstOrDefault(s => s.Id == id);
            return View(model);
        }

        public JsonResult GetProjectCost(int projectId, int matherialId, int complectationId)
        {
            var result = MainHelper.FormatPrice(MainHelper.GetProjectCost(projectId, matherialId, complectationId));
            var result1 = MainHelper.FormatPrice(MainHelper.GetProjectCost(projectId, matherialId, 1));
            var result2 = MainHelper.FormatPrice(MainHelper.GetProjectCost(projectId, matherialId, 2));
            var result3 = MainHelper.FormatPrice(MainHelper.GetProjectCost(projectId, matherialId, 3));

            return Json(new { cost = result, cost1 = result1, cost2 = result2, cost3 = result3  });
        }

        public PartialViewResult FilterProjects(ProjectFilterModel filter)
        {
            return PartialView("~/Views/Shared/_ProjectListPartial.cshtml", MainHelper._projects.Projects.Filter(filter));
        }
    }
}