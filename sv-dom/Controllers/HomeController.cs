﻿using System;
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

        public ActionResult Docs()
        {
            return View();
        }

        public ActionResult Staff()
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

        public ActionResult Gallery(string id)
        {
            var ids = new[] { 1, 2, 3, 4 };
            var title = "Коллекция построенных объектов";
            if (!string.IsNullOrEmpty(id))
            {
                var _id = 0;
                if (int.TryParse(id, out _id))
                {
                    ids = new[] { _id };
                }
                else
                {
                    switch (id)
                    {
                        case "poselki":
                            title = "Наши поселки";
                            ids = new[] { 5, 6, 7, 8, 9 };
                            break;
                        case "objects":
                            title = "Наши последние работы";
                            ids = new[] { 20, 19, 18, 17, 16, 14, 13, 12, 11 };
                            break;
                    }
                }
            }
            return View(new Tuple<int[], string>(ids, title));
        }

        public ActionResult Catalog()
        {
            return View();
        }

        public ActionResult Partners()
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

        public ActionResult Service(int id)
        {
            var name = "Services";

            switch(id)
            {
                case 1:
                    name += "\\Architecture";
                    break;
                case 2:
                    name += "\\Construction";
                    break;
                case 3:
                    name += "\\Timber";
                    break;
                case 4:
                    name += "\\Gluedbeam";
                    break;
                case 5:
                    name += "\\Fond";
                    break;
                case 6:
                    name += "\\Roof";
                    break;
                case 7:
                    name += "\\Engineering";
                    break;
                case 8:
                    name += "\\Indoor";
                    break;
                case 9:
                    name += "\\Outdoor";
                    break;
                case 10:
                    name += "\\TechControl";
                    break;
                case 11:
                    name += "\\Pools";
                    break;
                case 12:
                    name += "\\LandDesign";
                    break;
                case 13:
                    name += "\\Reconstruction";
                    break;
            }

            return View(name);
        }

        public ActionResult Articles(int id)
        {
            var name = "Articles";

            switch (id)
            {
                case 1:
                    name += "\\Timber";
                    break;
                case 2:
                    name += "\\Gluedbeam";
                    break;
                case 3:
                    name += "\\Profiledbeam";
                    break;
                case 4:
                    name += "\\Types";
                    break;
                case 5:
                    name += "\\Construction";
                    break;
            }

            return View(name);
        }
        public ActionResult ShowTour(int? projectid)
        {
            return View(projectid);
        }

        public JsonResult GetProjectCost(int projectId, int matherialId, int complectationId, Guid[] options)
        {
            var result = MainHelper.FormatPrice(MainHelper.GetProjectCost(projectId, matherialId, complectationId));
            var result1 = MainHelper.FormatPrice(MainHelper.GetProjectCost(projectId, matherialId, 1, (complectationId == 1 ? options : null)));
            var result2 = MainHelper.FormatPrice(MainHelper.GetProjectCost(projectId, matherialId, 2, (complectationId == 2 ? options : null)));
            var result3 = MainHelper.FormatPrice(MainHelper.GetProjectCost(projectId, matherialId, 3, (complectationId == 3 ? options : null)));

            return Json(new { cost = result, cost1 = result1, cost2 = result2, cost3 = result3  });
        }

        public JsonResult GetRecall(string name, string phone, string email, string comment, string subcomment, string textbody, string pagename)
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
                if (!string.IsNullOrEmpty(subcomment))
                {
                    body.AppendLine("");
                    body.AppendLine(subcomment.Trim());
                }
                if (!string.IsNullOrEmpty(textbody))
                {
                    body.AppendLine("");
                    body.AppendLine(textbody.Trim());
                    body.AppendLine("");
                }
                if (!string.IsNullOrEmpty(comment))
                {
                    body.AppendLine("Комментарий:");
                    body.AppendLine(comment.Trim());
                }

                if (!string.IsNullOrEmpty(pagename))
                {
                    body.AppendLine("");
                    body.AppendLine("");
                    body.AppendLine("(Отправлено со страницы - " + pagename.Trim() + ")");
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

        public PartialViewResult GetOptionsPartial(Guid[] list)
        {
            var model = MainHelper._options.Options.Where(s => list.Contains(s.ID)).ToList();
            return PartialView("~/Views/Shared/_OptionsPartial.cshtml", model);
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