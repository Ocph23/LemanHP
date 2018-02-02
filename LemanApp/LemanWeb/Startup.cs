using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LemanWeb.Startup))]
namespace LemanWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
