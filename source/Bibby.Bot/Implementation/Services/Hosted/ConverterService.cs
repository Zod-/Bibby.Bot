using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Bibby.UnitConversion.Contracts;
using Discord.WebSocket;
using Microsoft.Extensions.Hosting;

namespace Bibby.Bot.Services.Hosted
{
    public class ConverterService : IHostedService
    {
        private readonly DiscordSocketClient _discordClient;
        private readonly IMessageService _messageService;
        private readonly IReadOnlyList<IConvertUnits> _converters;

        public ConverterService(DiscordSocketClient discordClient, IMessageService messageService, IEnumerable<IConvertUnits> converters)
        {
            _discordClient = discordClient;
            _messageService = messageService;
            _converters = converters.ToList();
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
            var conversions = _converters.SelectMany(converter => converter.ConvertUnits(text));
            var response = conversions.Aggregate(string.Empty, (current, valueTuple) => current + $"{valueTuple.unit} == {valueTuple.converted}\n");
            await _messageService.SendAsync(socketMessage.Channel, response);
        }
    }
}
