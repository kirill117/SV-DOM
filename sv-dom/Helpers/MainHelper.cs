﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;
using System.IO;

namespace Helpers
{
    public enum ProjectCommentType
    {
        Project,
        Sample,
        Planning
    }
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
            list.ForEach(x =>
            {
                if (filter.Matherial2 && new int[] { 37, 46, 48 }.Contains(x.Id))
                {
                    x.Index = -1;
                }
                else
                {
                    x.Index = 0;
                }
            });
            return list.Where(s => FilteredProject(s, filter)).OrderBy(x => x.Index).ToList();
        }

        private static bool FilteredProject(ProjectModel project, ProjectFilterModel filter)
        {
            if (filter.IsEmpty())
                return true;

            var areaResult = false;
            if (filter.Area1 && project.Area <= 100)
                areaResult = true;

            if (filter.Area2 && project.Area > 100 && project.Area <= 200)
                areaResult = true;

            if (filter.Area3 && project.Area > 200)
                areaResult = true;

            if ((filter.Area1 || filter.Area2 || filter.Area3) && !areaResult)
                return false;

            var matherialResult = false;
            if (filter.Matherial1 && project.Matherials.Contains(Matherial.ОЦБ))
                matherialResult = true;

            if (filter.Matherial2 && project.Matherials.Contains(Matherial.КБ))
                matherialResult = true;

            if (filter.Matherial3 && project.Matherials.Contains(Matherial.КОМБИ))
                matherialResult = true;

            if ((filter.Matherial1 || filter.Matherial2 || filter.Matherial3) && !matherialResult)
                return false;

            var cost = GetProjectMinCost(project.Id);
            var priceResult = false;
            if (filter.Price1 && (cost <= 1000000 || cost == 0))
                priceResult = true;

            if (filter.Price2 && (cost == 0 || (cost > 1000000 && cost <= 2000000)))
                priceResult = true;

            if (filter.Price3 && (cost > 2000000 || cost == 0))
                priceResult = true;

            if ((filter.Price1 || filter.Price2 || filter.Price3) && !priceResult)
                return false;

            var floorResult = false;
            if (filter.Floor1 && project.FloorsCount == 1)
                floorResult = true;

            if (filter.Floor2 && project.FloorsCount == 2)
                floorResult = true;

            if (filter.Floor3 && project.FloorsCount > 2)
                floorResult = true;

            if ((filter.Floor1 || filter.Floor2 || filter.Floor3) && !floorResult)
                return false;

            return areaResult || matherialResult || priceResult || floorResult;
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

        public static string GetProject3DTourLink(int projectId)
        {
            var path = "";
            var basePath = $"/Images/{projectId}/3DTour";

            var localPath = HttpContext.Current.Server.MapPath(basePath);

            if (Directory.Exists(localPath))
            {
                var files = Directory.GetFiles(localPath, "*.swf", SearchOption.TopDirectoryOnly);
                if (files.Any())
                {
                    path = basePath + "/" + Path.GetFileName(files[0]);
                }
            }

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
                    list.Add(new SampleImageModel() { Name = path, Comment = GetProjectComment(projectId, ProjectCommentType.Planning) });
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
                    list.Add(new SampleImageModel() { Name = path, Comment = GetProjectComment(projectId, ProjectCommentType.Sample) });
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
                    list.Add(new SampleImageModel() { Name = path, Comment = GetProjectComment(projectId, ProjectCommentType.Sample) });
                }
            }

            return list.ToArray();
        }

        public static SampleImageModel[] GetMainGallery(int typeId)
        {
            var list = new List<SampleImageModel>();
            var basePath = $"/Content/Img/Gallery/{typeId}";

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

        public static string GetProjectComment(int projectId, ProjectCommentType type, Matherial? matherial = null)
        {
            var result = "";
            var project = _projects.Projects.FirstOrDefault(s => s.Id == projectId);

            var ht1 = "";
            var ht2 = "";
            var mt = "";

            matherial = matherial ?? project.Matherials.FirstOrDefault();

            switch (project.Type)
            {
                case 1:
                case 5:
                    ht1 = "Дом";
                    ht2 = "дома";
                    break;
                case 4:
                    ht1 = "Баня";
                    ht2 = "бани";
                    break;
                case 6:
                    ht1 = "Малая форма";
                    ht2 = "малой формы";
                    break;
            }

            switch(matherial)
            {
                case Matherial.КБ:
                    mt = "клееного бруса";
                    break;
                case Matherial.ПБ:
                    mt = "профилированного бруса";
                    break;
                case Matherial.КОМБИ:
                    mt = "комбинированных материалов";
                    break;
                default:
                    mt = "оцилиндрованного бревна";
                    break;
            }

            switch (type)
            {
                case ProjectCommentType.Project:
                    result = $"Проект {ht2} из {mt} «{project.Name}»";
                    break;
                case ProjectCommentType.Planning:
                    result = $"План {ht2} из {mt} «{project.Name}»";
                    break;
                case ProjectCommentType.Sample:
                    result = $"{ht1} из {mt} «{project.Name}»";
                    break;
            }

            return result;
        }
    }
}