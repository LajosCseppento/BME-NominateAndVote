using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(NominateAndVote.RestService.Startup))]

namespace NominateAndVote.RestService
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}