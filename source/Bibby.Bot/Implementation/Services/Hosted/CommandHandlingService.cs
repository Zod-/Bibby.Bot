using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Bibby.Bot.Options;
using Bibby.Bot.Utilities.Extensions;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using JetBrains.Annotations;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Bibby.Bot.Services.Hosted
{
    [UsedImplicitly]
    public class CommandHandlingService : IHostedService
    {
        private readonly CommandService _commandService;
        private readonly DiscordSocketClient _discordClient;
        private readonly IServiceProvider _services;
        private readonly DiscordOptions _options;
        private readonly ILogger<CommandHandlingService> _logger;
        private IEnumerable<ModuleInfo> _modules;

        public CommandHandlingService(IServiceProvider services, CommandService commandServiceService, DiscordSocketClient discordClientClient, IOptions<DiscordOptions> options, ILogger<CommandHandlingService> logger)
        {
            _commandService = commandServiceService;
            _discordClient = discordClientClient;
            _services = services;
            _options = options.Value;
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _commandService.Log += CommandServiceOnLog;
            _discordClient.MessageReceived += MessageReceivedAsync;
            _modules = await _commandService.AddModulesAsync(Assembly.GetEntryAssembly(), _services);
        }

        private Task CommandServiceOnLog(LogMessage logMessage)
        {
            _logger.Log(logMessage);
            return Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _commandService.Log -= CommandServiceOnLog;
            _discordClient.MessageReceived -= MessageReceivedAsync;
            foreach (var moduleInfo in _modules)
            {
                await _commandService.RemoveModuleAsync(moduleInfo);
            }
        }

        private async Task MessageReceivedAsync(SocketMessage rawMessage)
        {
            if (!(rawMessage is SocketUserMessage message)) return;
            if (message.Author.Id == _discordClient.CurrentUser.Id)
            {
                return;
            }

            var argPos = 0;
            if (!message.HasStringPrefix(_options.CommandPrefix, ref argPos) && !message.HasMentionPrefix(_discordClient.CurrentUser, ref argPos))
            {
                return;
            }

            var context = new SocketCommandContext(_discordClient, message);
            var result = await _commandService.ExecuteAsync(context, argPos, _services);

            if (result.Error.HasValue && result.Error.Value != CommandError.UnknownCommand)
            {
                _logger.LogError(result.Error.ToString());
            }
        }
    }
}