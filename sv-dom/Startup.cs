using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(sv_dom.Startup))]
namespace sv_dom
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
