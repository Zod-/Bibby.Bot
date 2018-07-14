using Bibby.Bot.Options;
using Bibby.Bot.Services;
using Discord;
using Discord.Rest;
using Discord.WebSocket;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Bibby.Bot
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging();
            services.Configure<DiscordOptions>(Configuration.GetSection("DiscordOptions"));

            var discordClient = new DiscordSocketClient();
            services.AddSingleton<IDiscordClient>(discordClient);
            services.AddSingleton<BaseDiscordClient>(discordClient);
            services.AddSingleton(discordClient);

            services.AddHostedService<LifetimeEventsHostedService>();
            services.AddHostedService<LogService>();
            services.AddHostedService<LoginService>();
            services.AddHostedService<ChatService>();
            services.AddHostedService<TemperatureService>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
        }
    }
}
