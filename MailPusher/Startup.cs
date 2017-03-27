using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MailPusher.Startup))]
namespace MailPusher
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
