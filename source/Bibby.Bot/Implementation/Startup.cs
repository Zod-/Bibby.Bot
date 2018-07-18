using System.IO;
using Bibby.Bot.Options;
using Bibby.Bot.Services;
using Discord;
using Discord.Commands;
using Discord.Rest;
using Discord.WebSocket;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Bibby.Bot
{
    public class Startup
    {
        private IConfiguration Configuration { get; set; }

        public Startup(IHostingEnvironment env)
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddJsonFile("secrets.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"secrets.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging();
            services.Configure<DiscordOptions>(Configuration.GetSection("DiscordOptions"));
            ConfigureDiscordClient(services);
            ConfigureCommandServices(services);
            ConfigureCustomServices(services);
        }

        private static void ConfigureCustomServices(IServiceCollection services)
        {
            services.AddSingleton<IMessageService, MessageService>();
            services.AddHostedService<TemperatureService>();
        }

        private static void ConfigureDiscordClient(IServiceCollection services)
        {
            var discordClient = new DiscordSocketClient();
            services.AddSingleton<IDiscordClient>(discordClient);
            services.AddSingleton<BaseDiscordClient>(discordClient);
            services.AddSingleton(discordClient);
            services.AddHostedService<DiscordClientLogService>();
            services.AddHostedService<DiscordLoginService>();
        }

        private static void ConfigureCommandServices(IServiceCollection services)
        {
            var commandService = new CommandService(new CommandServiceConfig()
            {
                CaseSensitiveCommands = false,
                DefaultRunMode = RunMode.Async
            });
            services.AddSingleton(commandService);
            services.AddHostedService<CommandHandlingService>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
        }
    }
}
