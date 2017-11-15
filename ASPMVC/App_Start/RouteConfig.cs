using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
//
//Wytwarzanie url
//
namespace ASPMVC
{
    public class RouteConfig
    {
        /// <summary>
        /// Method which describes routing table
        /// </summary>
        /// <param name="routes"></param>

        public static void RegisterRoutes(RouteCollection routes)
        {
            //Order of route-function is important!

            //../{param1}-{param2}
            //../{param1}.{param2}
            //../{param1}{param2} -- FOBBIDEN!!
            //../sites/{param1}/{param2}
            //
            //Algorytm zachłanny :
            //{  filename  }.{ext}
            //  asp.net.mvc . xml    {filename}=asp.net.mvc  {ext}=xml
            //------------->---->
            //
            //My{location}-{sublocation}
            //MyHouse-Szczytno   ->   {location}=House    {sublocation}=Szczytno

            //Ignore this path
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //Można też w taki sposób rozwiązywać konflikty(zabronione strony, szybki dostep 
            //do plików) poprzez implementacje specjalnej klasy
            //routes.Add(new Route(
            //    "data/{0}",
            //     new PageRouteHandler("sdsd")
            //));

            //Default route
            routes.MapRoute(
                name: "DefaultFirst",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "View", action = "Index", id = UrlParameter.Optional }
            );

            //This function allows to use atribute routes
            routes.MapMvcAttributeRoutes();

            //ASP.Net in default, treat *.html urls like static file.
            //We need to add exception in Web.config file.
            routes.MapRoute(
                name: "ProductDetails",
                url: "album-{id}.html",
                defaults: new { controller = "Store", action = "Details" }
                );

            //Controller and action are not definied, so
            //we need to define default values       
            routes.MapRoute(
                name: "ProductList",
                url: "gatunki/{genrename}",
                defaults: new { controller = "Store", action = "List" },
                constraints: new { genrename = @"[\w& ]+" }
                );
            
            //../product/list -OK
            //../product/list/1 - nie można trzeba zdefinować nowy routing(poniżej)
            routes.MapRoute(
                name: "Simple",
                url: "{controller}/{action}",
                defaults: new { controller = "Home", action = "Index" }
            );

            //../product/list/1 - OK
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            //To samo co wyżej tylko prymitywniej (wskazanie na handler)
            routes.Add(new Route("{controller}/{action}/{id}",
                new MvcRouteHandler())
            {
                Defaults = new RouteValueDictionary
                {
                    {"controller", "Home" },
                    {"action", "Index" },
                    { "id", "0" }
                }
            }
            );

            //routes.MapRoute(
            //    name: "DefaultExample",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { id = "1" });
            //Można wpisać controller/list a przekieruje do controller/list/1
            //DZIAŁA TYLKO ze slashem 
            
            //constrains - czyli jak mają wyglądać znaku w url (tutaj tylko format
            //../2014/12/12
            //definiujemy w defaults akcje i kontroler
            //
            //Można zdefiniować własne constrains (Klasa która ma zaimplementowany
            //interfejs  -   IRouteConstraint
            //
            //przykład constraint:
            //new = { Http = new HttpMethodConstraint("Get") }; - spełnia tylko gdy GET
            routes.MapRoute(
                name: "Dates",
                url: "{year}/{month}/{day}",
                defaults: new { controller = "blog", action = "Index" },
                constraints: new { year = @"\d{4}", month = @"\d{2}", day = @"\d{2}" }
                );
            
            //{*action} oznacza ze ten argument bierze całą reszte z url
            //list/                     {action}=""
            //list/product/             {action}=product
            //list/product/1/1/1/1      {action}=product/1/1/1/1
            routes.MapRoute(
                name: "CatchAll",
                url: "{controller}/{*action}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
