using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ListAssist.Startup))]
namespace ListAssist
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
        }
    }
}
