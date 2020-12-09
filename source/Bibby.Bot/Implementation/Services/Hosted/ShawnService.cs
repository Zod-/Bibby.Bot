using Discord.WebSocket;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Bibby.Bot.Services.Hosted
{
    public class ShawnService : IHostedService
    {
        private readonly DiscordSocketClient _discordClient;
        private readonly IMessageService _messageService;
        private readonly Random rand = new Random();

        public ShawnService(DiscordSocketClient discordClient, IMessageService messageService)
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

            if (!socketMessage.Content.StartsWith("x", StringComparison.InvariantCultureIgnoreCase))
            {
                return;
            }

            var rand = this.rand.Next(0, 10);
            string text;
            if (rand < 8)
            {
                text = "Shawn" + new string('!', rand);
            }
            else
            {
                text = "https://i.imgur.com/sjm4ahg.gif";
            }
            await _messageService.SendAsync(socketMessage.Channel, text);
        }
    }
}
