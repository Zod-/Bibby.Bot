using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Bibby.Bot.Options;
using Bibby.Bot.Utilities.Extensions;
using Bibby.CustomVision;
using Bibby.UnitConversion.Contracts;
using Discord;
using Discord.Rest;
using Discord.WebSocket;
using JetBrains.Annotations;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Bibby.Bot.Services.Hosted
{
    [UsedImplicitly]
    public class CatOrCroissantService : IHostedService
    {
        private readonly ILogger<CatOrCroissantService> _logger;
        private readonly DiscordSocketClient _discordClient;
        private readonly IMessageService _messageService;
        private readonly ICatOrCroissant _catOrCroissant;
        private readonly ChannelOptions _channelOptions;

        public CatOrCroissantService(ILogger<CatOrCroissantService> logger, DiscordSocketClient discordClient, IMessageService messageService, ICatOrCroissant catOrCroissant, IOptions<ChannelOptions> channelOptions)
        {
            _logger = logger;
            _discordClient = discordClient;
            _messageService = messageService;
            _catOrCroissant = catOrCroissant;
            _channelOptions = channelOptions.Value;
            if (_channelOptions.CatOrCroissantChannels.Length == 0)
            {
                logger.LogWarning("No channels configured for Cat Or Croissant.");
            }
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
            if (!TryGetImageUrl(socketMessage, out var imgUrl)) return;

            var catOrCroissant = await _catOrCroissant.CatOrCroissantAsync(imgUrl);

            var msg = (RestUserMessage)await socketMessage.Channel.GetMessageAsync(socketMessage.Id);
            if (catOrCroissant.Cat > 0.85)
            {
                await msg.AddReactionAsync(new Emoji("🐈"));
            }
            if (catOrCroissant.Croissant > 0.85)
            {
                await msg.AddReactionAsync(new Emoji("🥐"));
            }
            var sb = new StringBuilder();
            sb.AppendLine($"🐈 {catOrCroissant.Cat * 100:0.##}%");
            sb.AppendLine($"🥐 {catOrCroissant.Croissant * 100:0.##}%");
            var embed = new EmbedBuilder()
                .WithColor(Color.DarkOrange)
                .WithTitle("Predictions")
                .WithDescription(sb.ToString())
                .Build();
            await _messageService.SendAsync(socketMessage.Channel, embed);
        }

        private bool TryGetImageUrl(SocketMessage socketMessage, out string imgUrl)
        {
            imgUrl = string.Empty;
            if (socketMessage.Author.Id == _discordClient.CurrentUser.Id)
            {
                return false;
            }

            if (!_channelOptions.CatOrCroissantChannels.Contains(socketMessage.Channel.Id))
            {
                return false;
            }

            if (socketMessage.Attachments.Any(a => a.Filename.HasImageExtension()))
            {
                imgUrl = socketMessage.Attachments.First(a => a.Filename.HasImageExtension()).Url;
                return true;
            }

            if (Uri.TryCreate(socketMessage.Content, UriKind.Absolute, out var uriResult))
            {
                imgUrl = uriResult.ToString();
                return true;
            }

            if (socketMessage.Embeds.Any(e => e.Image.HasValue))
            {
                var embedImage = socketMessage.Embeds.First().Image;
                if (embedImage != null)
                {
                    imgUrl = embedImage.Value.Url;
                    return true;
                }
            }

            return false;
        }
    }
}
