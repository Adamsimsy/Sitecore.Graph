using Sitecore.DependencyInjection.ContainerContexts;
using Sitecore.DependencyInjection.DependencyResolvers;
using System.Web.Mvc;
using System.Web.Routing;
using Example.Website.App_Start.Installers;

namespace Example.Website
{
    public class Application : Sitecore.Web.Application
    {
        protected void Application_Start()
        {
            RegisterRoutes(RouteTable.Routes);
            WindsorContainerContext.Instance.Install(new ServicesInstaller());
        }

        private static void RegisterRoutes(RouteCollection routes)
        {
            //routes.MapRoute(
            //    name: "route example",
            //    url: "api/{controller}/{action}/{id}",
            //    defaults: new { controller = "RouteExample", action = "Index", id = UrlParameter.Optional });
        }
    }
}