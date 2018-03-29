using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SlowNewsBlog.Startup))]
namespace SlowNewsBlog
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
