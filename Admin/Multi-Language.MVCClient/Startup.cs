using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Multi_Language.MVCClient.Startup))]
namespace Multi_Language.MVCClient
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
