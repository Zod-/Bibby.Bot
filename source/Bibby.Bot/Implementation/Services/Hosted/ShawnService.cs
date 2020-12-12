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
            string text = string.Empty;
            var textSelection = rand.Next(0, 4);
            switch (textSelection)
            {
                case 0:
                    text = "Shawn" + new string('!', rand.Next(0, 7));
                    break;
                case 1:
                    text = "Shaun" + new string('!', rand.Next(0, 7));
                    break;
                case 2:
                    text = "https://i.imgur.com/sjm4ahg.gif";
                    break;
                case 3:
                    text = "https://i.imgur.com/zWz6pKl.gif";
                    break;
            }

            // cata
            if (rand.Next(0, 100) == 0)
            {
                text = "https://i.imgur.com/hXDxFOL.gif";
            }

            await _messageService.SendAsync(socketMessage.Channel, text);
        }
    }
}
