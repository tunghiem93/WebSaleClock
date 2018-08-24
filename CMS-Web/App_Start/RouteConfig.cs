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
            name: "Account",
            url: "tai-khoan/{q}",
            defaults: new
            {
                controller = "Account",
                action = "Index",
                q = UrlParameter.Optional,
                namespaces = new[] { "CMS_Web.Controllers" }
            });

            routes.MapRoute(
            name: "Location",
            url: "vi-tri/{q}",
            defaults: new
            {
                controller = "Location",
                action = "Index",
                q = UrlParameter.Optional,
                namespaces = new[] { "CMS_Web.Controllers" }
            });

            routes.MapRoute(
            name: "LoginSignIn",
            url: "dang-nhap/{q}",
            defaults: new
            {
                controller = "Login",
                action = "SignIn",
                q = UrlParameter.Optional,
                namespaces = new[] { "CMS_Web.Controllers" }
            });

            routes.MapRoute(
            name: "LoginSignUp",
            url: "dang-ky/{q}",
            defaults: new
            {
                controller = "Login",
                action = "SignUp",
                q = UrlParameter.Optional,
                namespaces = new[] { "CMS_Web.Controllers" }
            });

            routes.MapRoute(
            name: "Cart",
            url: "gio-hang/",
            defaults: new
            {
                controller = "Cart",
                action = "Index",
                q = UrlParameter.Optional,
                namespaces = new[] { "CMS_Web.Controllers" }
            });

            routes.MapRoute(
            name: "CartCheckOut",
            url: "thanh-toan/{q}",
            defaults: new
            {
                controller = "Cart",
                action = "CheckOut",
                q = UrlParameter.Optional,
                namespaces = new[] { "CMS_Web.Controllers" }
            });

            /* route product */
            routes.MapRoute(
            name: "Product",
            url: "san-pham/",
            defaults: new
            {
                controller = "Shop",
                action = "Index",
                q = UrlParameter.Optional,
                namespaces = new[] { "CMS_Web.Controllers" }
            });

            /* route product detail */
            routes.MapRoute(
            name: "Detail",
            url: "san-pham/{q}",
            defaults: new
            {
                controller = "Shop",
                action = "Detail",
                q = UrlParameter.Optional,
                namespaces = new[] { "CMS_Web.Controllers" }
            });


            /* Route collection */
            routes.MapRoute(
            name: "Collection",
            url: "bo-suu-tap/",
            defaults: new
            {
                controller = "Collection",
                action = "Index",
                q = UrlParameter.Optional,
                namespaces = new[] { "CMS_Web.Controllers" }
            });
            /* Routes collection detail */
            routes.MapRoute(
            name: "CollectionDetail",
            url: "bo-suu-tap/{q}",
            defaults: new
            {
                controller = "Collection",
                action = "Detail",
                q = UrlParameter.Optional,
                namespaces = new[] { "CMS_Web.Controllers" }
            });

            routes.MapRoute(
            name: "NewsHome",
            url: "tin-tuc/",
            defaults: new
            {
                controller = "News",
                action = "Index",
                q = UrlParameter.Optional,
                namespaces = new[] { "CMS_Web.Controllers" }
            });

            routes.MapRoute(
            name: "NewsDetail",
            url: "tin-tuc/{q}",
            defaults: new
            {
                controller = "News",
                action = "Detail",
                q = UrlParameter.Optional,
                namespaces = new[] { "CMS_Web.Controllers" }
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
