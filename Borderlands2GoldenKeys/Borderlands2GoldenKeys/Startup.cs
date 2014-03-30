using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Borderlands2GoldendKeys.Startup))]
namespace Borderlands2GoldendKeys
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
