using System;
using System.Threading;
using System.Threading.Tasks;
using Bibby.Bot.Options;
using Discord;
using Discord.Rest;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Bibby.Bot.Services
{
    public class LoginService : IHostedService, IDisposable
    {
        private readonly BaseDiscordClient _baseClient;
        private readonly IDiscordClient _discordClient;
        private readonly AuthenticationOptions _authOptions;

        public LoginService(BaseDiscordClient loginClient, IOptions<AuthenticationOptions> authOptions)
        {
            _discordClient = _baseClient = loginClient;
            _authOptions = authOptions.Value;

        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _baseClient.LoginAsync(TokenType.Bot, _authOptions.Token);
            await _discordClient.StartAsync();
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _discordClient.StopAsync();
        }

        public void Dispose()
        {
        }
    }
}
