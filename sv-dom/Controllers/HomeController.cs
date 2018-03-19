using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using Helpers;
using System.Text;
using System.Configuration;
using System.Web.Helpers;


namespace sv_dom.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //var p = new ProjectsListModel();
            //p.Projects = new List<ProjectModel>();
            //var prj = new ProjectModel() { Id = 1 };
            //prj.Matherials = new[] { Matherial.ОЦБ };
            //p.Projects.Add(prj);
            //XMLHelper.Save(p);

            //var c = new PriceListModel();
            //c.Prices = new List<PriceModel>();
            //c.Prices.Add(new PriceModel() { ProjectID = 1 });
            //XMLHelper.Save(c);

            //экспорт списка проектов
            //var sb = new StringBuilder();
            //foreach (var item in MainHelper._projects.Projects.OrderBy(s => s.Id))
            //{
            //    sb.AppendLine($"{item.Id} {item.Name}");
            //}
            //System.IO.File.WriteAllText("d:\\projects.txt", sb.ToString());

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
        public ActionResult Credit()
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
                filter.Type = type.Value < 4 ? 1 : type.Value;
                switch (type.Value)
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
                model = model.Where(s => s.Type == filter.Type).ToList().Filter(filter);
            }

            ViewBag.ProjectFilter = filter;
            return View(model);
        }

        public ActionResult Project(int id)
        {
            var model = MainHelper._projects.Projects.FirstOrDefault(s => s.Id == id);
            return View(model);
        }

        public ActionResult ShowTour(int? projectid)
        {
            return View(projectid);
        }

        public JsonResult GetProjectCost(int projectId, int matherialId, int complectationId)
        {
            var result = MainHelper.FormatPrice(MainHelper.GetProjectCost(projectId, matherialId, complectationId));
            var result1 = MainHelper.FormatPrice(MainHelper.GetProjectCost(projectId, matherialId, 1));
            var result2 = MainHelper.FormatPrice(MainHelper.GetProjectCost(projectId, matherialId, 2));
            var result3 = MainHelper.FormatPrice(MainHelper.GetProjectCost(projectId, matherialId, 3));

            return Json(new { cost = result, cost1 = result1, cost2 = result2, cost3 = result3  });
        }

        public JsonResult GetRecall(string name, string phone, string email, string comment)
        {
            var result = false;
            var user = ConfigurationManager.AppSettings["MailUserTarget"];
            if (!string.IsNullOrEmpty(user) && !string.IsNullOrEmpty(name) && (!string.IsNullOrEmpty(phone) || !string.IsNullOrEmpty(email)))
            {
                var body = new StringBuilder();
                body.AppendLine("Форма обратной связи SV-DOM.RU");
                body.AppendLine("");
                body.AppendLine("Имя клиента: " + name.Trim());
                body.AppendLine("Тел.: " + phone.Trim());
                if (!string.IsNullOrEmpty(email))
                {
                    body.AppendLine("Email: " + email.Trim());
                }
                if (!string.IsNullOrEmpty(comment))
                {
                    body.AppendLine("Комментарий:");
                    body.AppendLine(comment.Trim());
                }

                result = EmailHelper.SendMail(user, body.ToString(), "Сообщение с сайта SV-DOM.RU");
            }
            return Json(result);
        }
        public PartialViewResult FilterProjects(ProjectFilterModel filter)
        {
            if (filter.Type < 4)
                filter.Type = 1;

            return PartialView("~/Views/Shared/_ProjectListPartial.cshtml", MainHelper._projects.Projects.Where(s => filter.Type == 0 || s.Type == filter.Type).ToList().Filter(filter));
        }

        public void GetPhotoThumbnail(string filename, int width = 300, int height = 220)
        {
            var serverfilepath = Server.MapPath(filename);
            new WebImage(serverfilepath)
                .Resize(width, height, false, true) 
                //.AddTextWatermark("SV-DOM.RU", verticalAlign: "Top", opacity: 30)
                .Crop(1, 1) 
                .Write();
        }
    }
}