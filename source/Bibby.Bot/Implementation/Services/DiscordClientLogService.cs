﻿using System.Threading;
using System.Threading.Tasks;
using Discord;
using Discord.Rest;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Bibby.Bot.Services
{
    // ReSharper disable once UnusedMember.Global
    public class DiscordClientLogService : IHostedService
    {
        private readonly BaseDiscordClient _discordClient;
        private readonly ILogger _logger;

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
            _logger.LogInformation(logMessage.ToString());
            return Task.CompletedTask;
        }
    }
}