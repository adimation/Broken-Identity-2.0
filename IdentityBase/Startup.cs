using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IdentityBase.Startup))]
namespace IdentityBase
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
