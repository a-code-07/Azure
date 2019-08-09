using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GarageProject.Startup))]
namespace GarageProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
