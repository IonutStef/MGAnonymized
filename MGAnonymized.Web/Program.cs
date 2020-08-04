using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using MGAnonymized.Web.Infrastructure.Extensions;
using Serilog;
using System.Threading.Tasks;

namespace MGAnonymized.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await CreateWebHostBuilder(args).Build().RunWithTasksAsync();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseSerilog((ctx, c) => c.ReadFrom.Configuration(ctx.Configuration), true)
                .UseStartup<Startup>();
    }
}
