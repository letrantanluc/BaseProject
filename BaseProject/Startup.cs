using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BaseProject.Startup))]
namespace BaseProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
