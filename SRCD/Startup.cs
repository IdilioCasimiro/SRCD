using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SRCD.Startup))]
namespace SRCD
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
