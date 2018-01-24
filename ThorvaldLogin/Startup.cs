using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ThorvaldLogin.Startup))]
namespace ThorvaldLogin
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
