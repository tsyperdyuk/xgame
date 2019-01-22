using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xgame.Core;

namespace Xgame.Mvc
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appusers.json", optional: true)
                 .AddCommandLine(args)
                 .Build();            
            Dictionary<string, string> users = config.GetSection("Users").Get<Dictionary<string, string>>();

            var host = CreateWebHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                await scope.ServiceProvider.GetService<StartInitializer>().InitializeAsync(users);
            }
            host.Run();           
        }
        
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
