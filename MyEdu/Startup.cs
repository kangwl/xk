using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyEdu.Startup))]
namespace MyEdu
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
