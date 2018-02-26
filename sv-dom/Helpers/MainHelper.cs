using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;
using System.IO;

namespace Helpers
{
    public static class MainHelper
    {
        public static ProjectsListModel _projects = XMLHelper.Get<ProjectsListModel>();
        public static PriceListModel _prices = XMLHelper.Get<PriceListModel>();

        public static List<ProjectModel> GetProjectsForMainPage()
        {
            return _projects.Projects.Where(s => s.ShowOnMain).ToList();
        }

        public static List<ProjectModel> Filter(this List<ProjectModel> list, ProjectFilterModel filter)
        {
            return list.Where(s => FilteredProject(s, filter)).ToList();
        }

        private static bool FilteredProject(ProjectModel project, ProjectFilterModel filter)
        {
            if (filter.IsEmpty())
                return true;

            var result = false;

            if (filter.Area1 && project.Area <= 100)
                result = true;

            if (filter.Area2 && project.Area > 100 && project.Area <= 200)
                result = true;

            if (filter.Area3 && project.Area > 200)
                result = true;

            if ((filter.Area1 || filter.Area2 || filter.Area3) && !result)
                return false;


            if (filter.Matherial1 && project.Matherials.Contains(Matherial.ОЦБ))
                result = true;

            if (filter.Matherial2 && project.Matherials.Contains(Matherial.КБ))
                result = true;

            if (filter.Matherial3 && project.Matherials.Contains(Matherial.КОМБИ))
                result = true;

            if ((filter.Matherial1 || filter.Matherial2 || filter.Matherial3) && !result)
                return false;


            if (filter.Price1 && GetProjectMinCost(project.Id) <= 1000000)
                result = true;

            if (filter.Price2 && GetProjectMinCost(project.Id) > 1000000 && project.Area <= 2000000)
                result = true;

            if (filter.Price3 && GetProjectMinCost(project.Id) > 2000000)
                result = true;

            if ((filter.Price1 || filter.Price2 || filter.Price2) && !result)
                return false;


            if (filter.Floor1 && project.FloorsCount == 1)
                result = true;

            if (filter.Floor2 && project.FloorsCount == 2)
                result = true;

            if (filter.Floor3 && project.FloorsCount > 2)
                result = true;

            if ((filter.Floor1 || filter.Floor2 || filter.Floor3) && !result)
                return false;

            return result;
        }

        public static string FormatPrice(decimal price)
        {
            return (price <= 0 ? "Цена по запросу" : price.ToString("### ### ##0") + " ₽");
        }

        public static decimal GetProjectMinCost(int projectId)
        {
            return GetProjectCost(projectId, 1, 1);
        }

        public static decimal GetProjectCost(int projectId, int matherialId, int complectationId)
        {
            var result = 0m;
            var prc = _prices.Prices.Where(s => s.ProjectID == projectId && s.ConfigurationID == complectationId && s.MatherialID == matherialId);
            if (prc != null && prc.Any())
            {
                var price = prc.Min(x => x.Cost);
                if (price > 0)
                    result = price;
            }
            return result;
        }

        public static string GetProjectTopPicture(int projectId)
        {
            var path = "";
            var basePath = $"/Images/{projectId}";

            var localPath = HttpContext.Current.Server.MapPath(basePath);

            if (Directory.Exists(localPath))
            {
                var files = Directory.GetFiles(localPath, "*.jpg", SearchOption.TopDirectoryOnly);
                if (files.Any())
                {
                    path = basePath + "/" + Path.GetFileName(files[0]);
                }
            }

            if (string.IsNullOrEmpty(path))
                path = "~/Content/img/no_image.jpg";
            return path;
        }

        public static SampleImageModel[] GetProjectPlanings(int projectId)
        {
            var list = new List<SampleImageModel>();
            var basePath = $"/Images/{projectId}/Planing";

            var localPath = HttpContext.Current.Server.MapPath(basePath);

            if (Directory.Exists(localPath))
            {
                var files = Directory.GetFiles(localPath, "*.jpg", SearchOption.TopDirectoryOnly);
                foreach(var file in files)
                {
                    var path = basePath + "/" + Path.GetFileName(file);
                    list.Add(new SampleImageModel() { Name = path, Comment = "" });
                }
            }

            return list.ToArray();
        }

        public static SampleImageModel[] GetProjectSamples(int projectId)
        {
            var list = new List<SampleImageModel>();
            var basePath = $"/Images/{projectId}/Samples";

            var localPath = HttpContext.Current.Server.MapPath(basePath);

            if (Directory.Exists(localPath))
            {
                var files = Directory.GetFiles(localPath, "*.jpg", SearchOption.TopDirectoryOnly);
                foreach (var file in files)
                {
                    var path = basePath + "/" + Path.GetFileName(file);
                    list.Add(new SampleImageModel() { Name = path, Comment = "" });
                }
            }

            return list.ToArray();
        }

        public static SampleImageModel[] GetProjectGallery(int projectId)
        {
            var list = new List<SampleImageModel>();
            var basePath = $"/Images/{projectId}/Gallery";

            var localPath = HttpContext.Current.Server.MapPath(basePath);

            if (Directory.Exists(localPath))
            {
                var files = Directory.GetFiles(localPath, "*.jpg", SearchOption.TopDirectoryOnly);
                foreach (var file in files)
                {
                    var path = basePath + "/" + Path.GetFileName(file);
                    list.Add(new SampleImageModel() { Name = path, Comment = "" });
                }
            }

            return list.ToArray();
        }
    }
}