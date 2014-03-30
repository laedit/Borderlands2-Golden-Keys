using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Borderlands2GoldenKeys.Startup))]
namespace Borderlands2GoldenKeys
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
