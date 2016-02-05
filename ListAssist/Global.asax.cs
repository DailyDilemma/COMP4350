using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Data.Entity;
using ListAssist.Data;

namespace ListAssist
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Drop, recreate and reseed the database
            Database.SetInitializer(new DbInitializer());
            using (var db = new ListAssistContext())
            {
                db.Database.Initialize(false);
            }
        }

        protected void Session_Start()
        {
            // Redirect to the startup page for the project
            Response.Redirect("~/LALists");
        }

    }
}
