using System.Web.Optimization;

namespace TPVAXWebsite
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            // jQuery Bundle
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            // Bootstrap Bundle
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.bundle.js"));

            // Custom Scripts Bundle
            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                      "~/Scripts/app.js"));

            // Bootstrap CSS
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/main_style.css",
                      "~/Content/layout_page_style.css"));

            // Enable optimizations
            BundleTable.EnableOptimizations = false; // Set to true in production
        }
    }
}
