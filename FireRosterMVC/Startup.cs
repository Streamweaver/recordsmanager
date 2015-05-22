using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FireRosterMVC.Startup))]
namespace FireRosterMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}