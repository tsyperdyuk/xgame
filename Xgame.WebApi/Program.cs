using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Xgame.WebApi;

namespace WebApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();
            DbInitializer.TryInitDb(host).GetAwaiter().GetResult();
            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
