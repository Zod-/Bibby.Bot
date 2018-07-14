using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Bibby.Bot
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile("secrets.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            CreateWebHostBuilder(args, config).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args, IConfiguration config) =>
            WebHost.CreateDefaultBuilder(args)
            .UseConfiguration(config)
            .UseStartup<Startup>();
        //public static async Task Main(string[] args)
        //{
        //    var host = new HostBuilder()
        //        .ConfigureAppConfiguration(ConfigureAppConfiguration)
        //        .ConfigureServices(ConfigureServices)
        //        .ConfigureLogging(ConfigureLogging)
        //        .UseConsoleLifetime()
        //        .UseEnvironment(EnvironmentName.Development)
        //        .Build();

        //    await host.RunAsync();
        //}

        //private static void ConfigureAppConfiguration(HostBuilderContext context, IConfigurationBuilder config)
        //{
        //    config
        //        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
        //        .AddJsonFile($"appsettings.{context.HostingEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true)
        //        .AddJsonFile("secrets.json", optional: true, reloadOnChange: true)
        //        .AddJsonFile($"secrets.{context.HostingEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true)
        //        .AddEnvironmentVariables();
        //}

        //private static void ConfigureLogging(HostBuilderContext context, ILoggingBuilder logging)
        //{
        //    logging.AddConsole();
        //    logging.AddDebug();
        //}

        //private static void ConfigureServices(HostBuilderContext context, IServiceCollection services)
        //{
        //    services.AddLogging();
        //    services.Configure<DiscordOptions>(context.Configuration.GetSection("DiscordOptions"));
        //    var discordClient = new DiscordSocketClient();

        //    services.AddSingleton<IDiscordClient>(discordClient);
        //    services.AddSingleton<BaseDiscordClient>(discordClient);
        //    services.AddSingleton(discordClient);

        //    services.AddHostedService<LifetimeEventsHostedService>();
        //    services.AddHostedService<DiscordClientLogService>();
        //    services.AddHostedService<DiscordLoginService>();
        //    services.AddHostedService<ChatService>();
        //    services.AddHostedService<TemperatureService>();
        //}
    }
}
