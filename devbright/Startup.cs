using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(devbright.Startup))]
namespace devbright
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
