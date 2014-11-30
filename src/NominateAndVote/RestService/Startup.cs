using Microsoft.Owin;
using NominateAndVote.RestService;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

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