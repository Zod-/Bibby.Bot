using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Bibby.Bot.Utilities.Temperature;
using Discord.WebSocket;
using Microsoft.Extensions.Hosting;

namespace Bibby.Bot.Services.Hosted
{
    public class TemperatureService : IHostedService
    {
        private readonly DiscordSocketClient _discordClient;
        private readonly IMessageService _messageService;

        public TemperatureService(DiscordSocketClient discordClient, IMessageService messageService)
        {
            _discordClient = discordClient;
            _messageService = messageService;
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
            var response = string.Join(Environment.NewLine, temperatureMentions.Select(m => m.ToStringConverted()));

            await _messageService.SendAsync(socketMessage.Channel, response);
        }
    }
}
