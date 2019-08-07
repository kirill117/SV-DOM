using System.Web;
using System.Web.Optimization;

namespace sv_dom
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/fancybox").Include(
                      "~/content/fancybox/jquery.fancybox.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/imgViewer").Include(
                      "~/content/js/underscore-min.js",
                      "~/content/js/imgViewer.js"));

            bundles.Add(new StyleBundle("~/bundles/css").Include(
                      "~/Content/css/normalize.css",
                      "~/Content/css/font-awesome.css",
                      "~/Content/css/style.css",
                      "~/Content/css/mobile.css",
                      "~/content/fancybox/jquery.fancybox.min.css"
                      ).Include("~/Content/css/font-awesome.css", new CssRewriteUrlTransform()));

            BundleTable.EnableOptimizations = true;
        }
    }
}
