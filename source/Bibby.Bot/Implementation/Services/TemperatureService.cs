using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Bibby.Bot.Utilities.Temperature;
using Discord.WebSocket;
using Microsoft.Extensions.Hosting;

namespace Bibby.Bot.Services
{
    public class TemperatureService : IHostedService
    {
        private readonly DiscordSocketClient _discordClient;

        public TemperatureService(DiscordSocketClient discordClient)
        {
            _discordClient = discordClient;
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
            if (socketMessage.Author.Id == _discordClient.CurrentUser.Id)
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
