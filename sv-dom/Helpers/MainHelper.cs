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

        public static string GetProjectMinCost(int projectId)
        {
            var result = "Цена по запросу";
            var prc = _prices.Prices.Where(s => s.ProjectID == projectId);
            if (prc != null && prc.Any())
            {
                var price = prc.Min(x => x.Cost);
                if (price > 0)
                    result = price.ToString("# #0") + " руб.";
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
    }
}