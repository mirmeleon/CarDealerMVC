using System.Web.Mvc;
using System.Web.Routing;

namespace CarDealerApp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            //2
            //routes.MapRoute(
            //    name: "Cars",
            //    url: "cars/{make}",
            //    defaults: new { controller = "Cars", action = "Make" }
            //);

            //3 filter suppliers
           // routes.MapRoute(
           //    name: "Suppliers",
           //    url: "supplier/{local}", //imeto na tva triabva da e ednakvo kat imeto na promenlivata det priema actiona
           //    defaults: new { controller = "Supplier", action = "Local" }
           //);

            //4 List of parts
            //routes.MapRoute(
            //name: "ListOfParts",
            //url: "cars/{id}/parts",
            //defaults: new { controller = "Cars", action = "Parts", id = UrlParameter.Optional }
            // );

            //5 }/customers/{id}
            //routes.MapRoute(
            //name: "Customer by Id",
            //url: "customers/{id}",
            //defaults: new { controller = "Customers", action = "FindById", id = UrlParameter.Optional }
            // );

            //6 sales
            // routes.MapRoute(
            //name: "Sales with discount",
            //url: "Sales/{action}/{id}/{percent}",
            //defaults: new { controller = "Sales", action = "All", id = UrlParameter.Optional }
            // );


            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
             name: "Default",
             url: "{controller}/{action}/{id}",
             defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
              );

        }
    }
}
