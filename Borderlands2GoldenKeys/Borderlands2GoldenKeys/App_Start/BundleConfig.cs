using System.Web;
using System.Web.Optimization;

namespace Borderlands2GoldenKeys
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.unobtrusive-ajax.js",
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/site").Include(
                      "~/Scripts/site.js",
                      "~/Scripts/jquery.zclip.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/ga").Include(
                      "~/Scripts/ga.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootswatch.slate.min.css",
                      "~/Content/site.css"));
#if !DEBUG
            BundleTable.EnableOptimizations = true;
#endif
        }
    }
}
