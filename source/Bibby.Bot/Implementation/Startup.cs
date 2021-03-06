﻿using System.IO;
using Bibby.Bot.Options;
using Bibby.Bot.Services;
using Bibby.Bot.Services.Hosted;
using Bibby.Bot.Services.Translations;
using Bibby.Bot.Services.TTS;
using Bibby.CustomVision;
using Bibby.UnitConversion.Contracts;
using Bibby.UnitConversion.Converters;
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
                .AddJsonFile("tts-languages.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging();
            ConfigureOptions(services);
            ConfigureDiscordClient(services);
            ConfigureCommandServices(services);
            ConfigureCustomServices(services);
            ConfigureConverters(services);
        }

        private void ConfigureOptions(IServiceCollection services)
        {
            services.Configure<DiscordOptions>(Configuration.GetSection("DiscordOptions"));
            services.Configure<AzureOptions>(Configuration.GetSection("AzureOptions"));
            services.Configure<TtsLanguages>(Configuration.GetSection("TtsLanguages"));
            services.Configure<CustomVisionOptions>(Configuration.GetSection("CustomVision"));
            services.Configure<ChannelOptions>(Configuration.GetSection("ChannelOptions"));
        }

        private static void ConfigureCustomServices(IServiceCollection services)
        {
            services.AddSingleton<IMessageService, MessageService>();
            services.AddSingleton<ITranslator, Translator>();
            services.AddSingleton<ITextToSpeech, TextToSpeech>();
            services.AddSingleton<LanguageSelection>();
            services.AddHostedService<ConverterService>();
            services.AddHttpClient<ICatOrCroissant, CatOrCroissant>();
        }

        private static void ConfigureConverters(IServiceCollection services)
        {
            services.AddTransient<IConvertUnits, TemperatureConverter>();
            services.AddTransient<IConvertUnits, LengthConverter>();
            services.AddTransient<IConvertUnits, SpeedConverter>();
            services.AddTransient<IConvertUnits, MassConverter>();
        }

        private static void ConfigureDiscordClient(IServiceCollection services)
        {
            var discordClient = new DiscordSocketClient();
            services.AddSingleton<IDiscordClient>(discordClient);
            services.AddSingleton<BaseDiscordClient>(discordClient);
            services.AddSingleton(discordClient);
            services.AddHostedService<DiscordClientLogService>();
            services.AddHostedService<DiscordLoginService>();
            services.AddHostedService<PlayingStatusService>();
            services.AddHostedService<CatOrCroissantService>();
            services.AddHostedService<ShawnService>();
            services.AddHostedService<DeleteThisService>();
        }

        private static void ConfigureCommandServices(IServiceCollection services)
        {
            services.AddSingleton(new CommandServiceConfig
            {
                CaseSensitiveCommands = false,
                DefaultRunMode = RunMode.Async
            });
            services.AddSingleton<CommandService>();
            services.AddHostedService<CommandHandlingService>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
        }
    }
}
