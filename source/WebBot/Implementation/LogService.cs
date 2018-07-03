using System;
using System.Threading;
using System.Threading.Tasks;
using Discord;
using Discord.Rest;
using Microsoft.Extensions.Hosting;

namespace WebBot
{
    public class LogService : IHostedService
    {
        private readonly BaseDiscordClient _discordClient;

        public LogService(BaseDiscordClient discordClient)
        {
            _discordClient = discordClient;
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
            Console.WriteLine(logMessage.ToString());
            return Task.CompletedTask;
        }
    }
}
