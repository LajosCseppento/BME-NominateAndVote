﻿using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(RestService.Startup))]

namespace RestService
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}