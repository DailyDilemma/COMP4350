using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Data.Entity;
using ListAssist.Data;
using System.Xml;
using System.Data.Entity.Infrastructure;
using System.Text;

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

#if DEBUG 
            //// Drop, recreate and reseed the database
            //Database.SetInitializer(new DbInitializer());
            //using (var db = new ListAssistContext())
            //{
            //    db.Database.Initialize(true);
            //}

            //// Recreate a edmx diagram from the latest model
            //using (var ctx = new ListAssistContext())
            //{
            //    using (var writer = new XmlTextWriter(HttpRuntime.AppDomainAppPath.TrimEnd('\\') + @".Data\EntityModelDiagram.edmx", Encoding.Default))
            //    {
            //        EdmxWriter.WriteEdmx(ctx, writer);
            //    }
            //}
#endif 
        }

        protected void Session_Start()
        {
            // Redirect to the startup page for the project
            Response.Redirect("~/LALists");
        }

    }
}
