using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PharmacySysyem.Startup))]
namespace PharmacySysyem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
