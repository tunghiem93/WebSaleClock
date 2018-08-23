using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CMS_Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
            name: "Detail",
            url: "san-pham/{q}",
            defaults: new
            {
                controller = "Shop",
                action = "Detail",
                q = UrlParameter.Optional,
            });

            routes.MapRoute(
            name: "CollectionDetail",
            url: "bo-suu-tap/{q}",
            defaults: new
            {
                controller = "Collection",
                action = "Detail",
                q = UrlParameter.Optional,
            });

            routes.MapRoute(
            name: "NewsDetail",
            url: "tin-tuc/{q}",
            defaults: new
            {
                controller = "News",
                action = "Detail",
                q = UrlParameter.Optional,
            });

            routes.MapRoute(
                 "Default", // Route name
                 "{controller}/{action}/{id}", // URL with parameters
                 new { area = "", controller = "Home", action = "Index", id = UrlParameter.Optional }, // Parameter defaults
                 null,
                 new[] { "CMS_Web.Controllers" }
             ).DataTokens.Add("area", "");
        }
    }
}
