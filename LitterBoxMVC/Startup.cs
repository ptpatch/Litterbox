using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LitterBoxMVC.Startup))]
namespace LitterBoxMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
