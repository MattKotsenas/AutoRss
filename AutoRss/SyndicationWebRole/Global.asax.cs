using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace AutoRss.SyndicationWebRole
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            IoCConfig.RegisterIoC(GlobalConfiguration.Configuration);
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            FormatterConfig.RegisterFormatters(GlobalConfiguration.Configuration);
        }
    }
}
