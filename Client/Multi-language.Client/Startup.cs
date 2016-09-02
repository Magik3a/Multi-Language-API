using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Multi_language.Client.Startup))]
namespace Multi_language.Client
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
