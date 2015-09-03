using System.Web.Optimization;

namespace TaconiteMVC.DemoWeb
{
  public class BundleConfig
  {
    public static void RegisterBundles(BundleCollection bundles)
    {
      bundles.Add(new StyleBundle("~/Content/CSS/styles").Include(
                  "~/Content/bootstrap.css",
                  "~/Content/prettify.css",
                  "~/Content/site.css"));

      bundles.Add(new ScriptBundle("~/Scripts/complete").Include(
                  "~/Scripts/jquery-1.8.2.js",
                  "~/Scripts/jquery.unobtrusive-ajax.js",
                  "~/Scripts/bootstrap.js",
                  "~/Scripts/application.js",
                  "~/Scripts/Prettify/prettify.js",
                  "~/Scripts/application.js",
                  "~/Scripts/jquery.flip.js",
                  "~/Scripts/jquery.taconite.js"));
    }
  }
}