﻿using System.Threading;
using System.Threading.Tasks;
using Bibby.Bot.Utilities.Extensions;
using Discord;
using Discord.Rest;
using JetBrains.Annotations;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Bibby.Bot.Services.Hosted
{
    [UsedImplicitly]
    public class DiscordClientLogService : IHostedService
    {
        private readonly BaseDiscordClient _discordClient;
        private readonly ILogger<DiscordClientLogService> _logger;

        public DiscordClientLogService(BaseDiscordClient discordClient, ILogger<DiscordClientLogService> logger)
        {
            _discordClient = discordClient;
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _discordClient.Log += DiscordClientOnLog;
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _discordClient.Log -= DiscordClientOnLog;
            return Task.CompletedTask;
        }

        private Task DiscordClientOnLog(LogMessage logMessage)
        {
            _logger.Log(logMessage);
            return Task.CompletedTask;
        }
    }
}
