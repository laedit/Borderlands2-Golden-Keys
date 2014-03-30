using System.Web.Mvc;
using System.Web.Routing;

namespace Borderlands2GoldenKeys
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Rss",
                url: "rss",
                defaults: new { controller = "Home", action = "Rss" }
            );

            routes.MapRoute(
                name: "ShowAll",
                url: "ShowAll",
                defaults: new { controller = "Home", action = "ShowAll" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/",
                defaults: new { controller = "Home", action = "Index" }
            );
        }
    }
}
