using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Worship.Startup))]
namespace Worship
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
