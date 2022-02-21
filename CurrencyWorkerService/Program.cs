using CurrencyWorkerService.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;

namespace CurrencyWorkerService
{
    public class Program
    {
        private static IConfiguration Configuration;
        public static void Main(string[] args)
        {
            Configuration = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json", true, true)
               .Build();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<WorkerService>();
                    services.Configure<NbpApiConfiguration>(Configuration.GetSection("NbpApiConfiguration"));
                    services.Configure<CurrencyConfiguration>(Configuration.GetSection("CurrencyConfiguration"));
                })
                .ConfigureLogging(logging =>
                {
                    logging.SetMinimumLevel(LogLevel.Trace);
                })
                .UseNLog();
    }
}
