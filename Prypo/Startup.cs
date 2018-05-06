using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Prypo.Startup))]
namespace Prypo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
