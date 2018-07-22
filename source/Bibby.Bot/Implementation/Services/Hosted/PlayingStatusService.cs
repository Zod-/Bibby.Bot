using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace Bibby.Bot.Services.Hosted
{
    public class PlayingStatusService : IHostedService
    {
        private readonly DiscordSocketClient _discordClient;

        public PlayingStatusService(DiscordSocketClient discordClient, IMessageService messageService)
        {
            _discordClient = discordClient;
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _discordClient.SetActivityAsync(new Game("your mom", ActivityType.Watching));
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
