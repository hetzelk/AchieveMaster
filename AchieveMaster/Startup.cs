using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AchieveMaster.Startup))]
namespace AchieveMaster
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
