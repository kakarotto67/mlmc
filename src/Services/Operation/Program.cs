using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Mlmc.Operation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                var env = hostingContext.HostingEnvironment;
                var esbSettingsLocation = Path.Combine(env.ContentRootPath, "..",
                    "EnterpriseServiceBus", "esb-settings.json");

                config.AddJsonFile(esbSettingsLocation, optional: false, reloadOnChange: false);
            })
            .UseStartup<Startup>();
    }
}