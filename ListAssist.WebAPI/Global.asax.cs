﻿using ListAssist.WebAPI.MappingProfile;
using ListAssist.Data;

using System.Data.Entity;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using ListAssist.WebAPI.Controllers;

namespace ListAssist.WebAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AutoMapperConfiguration.Configure();

#if DEBUG
            // Drop, recreate and reseed the database
            
            //Database.SetInitializer(new DbInitializer());
            //using (var db = new ListAssistContext("ListAssistContext"))
            //{
            //    db.Database.Initialize(false);
            //}
#endif
        }
    }
}
