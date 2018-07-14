using System.IO;
using System.Threading.Tasks;
using Bibby.Bot.Options;
using Bibby.Bot.Services;
using Discord;
using Discord.Rest;
using Discord.WebSocket;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using EnvironmentName = Microsoft.Extensions.Hosting.EnvironmentName;

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
        //    services.AddHostedService<LogService>();
        //    services.AddHostedService<LoginService>();
        //    services.AddHostedService<ChatService>();
        //    services.AddHostedService<TemperatureService>();
        //}
    }
}
