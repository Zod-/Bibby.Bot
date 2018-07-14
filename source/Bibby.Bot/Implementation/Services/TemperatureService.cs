using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Bibby.Bot.Options;
using Bibby.Bot.Utilities.Temperature;
using Discord.WebSocket;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Bibby.Bot.Services
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class TemperatureService : IHostedService
    {
        private readonly DiscordSocketClient _discordClient;
        private readonly DiscordOptions _botOptions;

        public TemperatureService(DiscordSocketClient discordClient, IOptions<DiscordOptions> options)
        {
            _discordClient = discordClient;
            _botOptions = options.Value;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _discordClient.MessageReceived += DiscordClientOnMessageReceived;
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _discordClient.MessageReceived -= DiscordClientOnMessageReceived;
            return Task.CompletedTask;
        }

        private async Task DiscordClientOnMessageReceived(SocketMessage socketMessage)
        {
            if (socketMessage.Author.Username == _botOptions.BotName)
            {
                return;
            }

            var text = socketMessage.Content;
            var temperatureMentions = TemperatureFinder.GetTemperatureMentions(text);
            var outputString = string.Join(Environment.NewLine, temperatureMentions.Select(m => m.ToStringConverted()));

            if (string.IsNullOrEmpty(outputString))
            {
                return;
            }

            await socketMessage.Channel.SendMessageAsync(outputString);
        }
    }
}
