using System.Threading;
using System.Threading.Tasks;
using Bibby.Bot.Options;
using Discord;
using Discord.Rest;
using JetBrains.Annotations;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Bibby.Bot.Services.Hosted
{
    [UsedImplicitly]
    public class DiscordLoginService : IHostedService
    {
        private readonly BaseDiscordClient _baseClient;
        private readonly IDiscordClient _discordClient;
        private readonly DiscordOptions _authOptions;

        public DiscordLoginService(BaseDiscordClient loginClient, IOptions<DiscordOptions> authOptions)
        {
            _discordClient = _baseClient = loginClient;
            _authOptions = authOptions.Value;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _baseClient.LoginAsync(TokenType.Bot, _authOptions.BotToken);
            await _discordClient.StartAsync();
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _discordClient.StopAsync();
        }
    }
}
