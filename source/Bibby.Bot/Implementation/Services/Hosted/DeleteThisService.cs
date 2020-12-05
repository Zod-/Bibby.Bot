using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Hosting;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Bibby.Bot.Services.Hosted
{
    public class DeleteThisService : IHostedService
    {
        private readonly DiscordSocketClient _discordClient;
        private readonly IMessageService _messageService;
        private readonly Regex _helloRegex = new Regex(@"h[eu]llo\??", RegexOptions.IgnoreCase);

        public DeleteThisService(DiscordSocketClient discordClient, IMessageService messageService)
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
            if (_helloRegex.IsMatch(socketMessage.Content))
            {
                await socketMessage.AddReactionAsync(GuildEmote.Parse("<:delet:347047426307129344>"));
            }
        }
    }
}
