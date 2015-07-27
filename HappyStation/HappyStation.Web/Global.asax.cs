using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

using Autofac;

using HappyStation.Web.App_Start;

namespace HappyStation.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            IContainer container = IocConfig.Configure();

            MapperConfig.Register(container);
            AreaRegistration.RegisterAllAreas();
            IocConfig.Configure();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            WebSecurityConfig.Configure();
        }
    }
}