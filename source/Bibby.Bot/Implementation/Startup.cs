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

            var discordClient = new DiscordSocketClient();
            services.AddSingleton<IDiscordClient>(discordClient);
            services.AddSingleton<BaseDiscordClient>(discordClient);
            services.AddSingleton(discordClient);

            services.AddSingleton<CommandService>();
            services.AddSingleton<IMessageService, MessageService>();

            services.AddHostedService<CommandHandlingService>();
            services.AddHostedService<DiscordClientLogService>();
            services.AddHostedService<DiscordLoginService>();
            services.AddHostedService<ChatService>();
            services.AddHostedService<TemperatureService>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
        }
    }
}
