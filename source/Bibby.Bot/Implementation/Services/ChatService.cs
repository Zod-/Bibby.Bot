using System.Threading;
using System.Threading.Tasks;
using Discord.WebSocket;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Bibby.Bot.Services
{
    class ChatService : IHostedService
    {
        private readonly DiscordSocketClient _discordClient;
        private readonly ILogger _logger;

        public ChatService(DiscordSocketClient discordClient, ILogger<ChatService> logger)
        {
            _discordClient = discordClient;
            _logger = logger;
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
            _logger.LogInformation(socketMessage.ToString());
            if (socketMessage.Content.ToLower().StartsWith("!ping"))
            {
                await socketMessage.Channel.SendMessageAsync("Pong");
            }
            if (socketMessage.Content.ToLower().StartsWith("!gdpr"))
            {
                await socketMessage.Channel.SendMessageAsync("I ain't saving shit.");
            }
        }
    }
}
