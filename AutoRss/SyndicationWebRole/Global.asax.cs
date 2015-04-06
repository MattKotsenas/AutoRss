﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace AutoRss.SyndicationWebRole
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            GlobalConfiguration.Configuration.Formatters.Add(new SyndicationFeedFormatter.RssSyndicationFeedFormatter("AutoRss", "AutoRss media"));
            GlobalConfiguration.Configuration.Formatters.Add(new SyndicationFeedFormatter.AtomSyndicationFeedFormatter("AutoRss", "AutoRss media"));
        }
    }
}
