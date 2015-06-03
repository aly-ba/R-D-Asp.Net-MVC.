using System;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using Microsoft.Framework.DependencyInjection;
using Microsoft.AspNet.Routing;
using Messenger.Services;
using Microsoft.Framework.ConfigurationModel;

namespace Messenger
{
    public class Startup
    {
        public Startup()
        {
            Configuration = new Configuration()
                                .AddJsonFile("config.json")
                                .AddEnvironmentVariables();
        }

        public IConfiguration Configuration { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddTransient<IGreetingService>(
                p => new GreetingService(Configuration));
        }
        
        public void Configure(IApplicationBuilder app)
        {
            app.UseMvc(); 
            //app.UseMvc(routes => routes.MapRoute("default",
            //    "{controller=Home}/{action=Default}"));

            //app.Run(async ctx => 
            //    await ctx.Response.WriteAsync("Hello World!"));

            //app.UseWelcomePage();

        }
    }
}
