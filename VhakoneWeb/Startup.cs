using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(VhakoneWeb.Startup))]
namespace VhakoneWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
