using System.Linq;
using System.Threading.Tasks;
using Bibby.Bot.Services;
using Discord;
using Discord.Commands;
using JetBrains.Annotations;

namespace Bibby.Bot.Commands
{
    [UsedImplicitly]
    public class TogglePrayerRole : ModuleBase<ICommandContext>
    {
        private const ulong BotChannel = 284308590594883595;
        private const ulong PrayerChannel = 738140579405365250;
        private const string Role = "prayer";

        public IMessageService MessageService { get; set; }

        [Command("pray")]
        public async Task RunAsync()
        {
            if (Context.Channel.Id != BotChannel)
            {
                return;
            }

            var user = Context.User;
            var role = Context.Guild.Roles.FirstOrDefault(x => x.Name.ToLower() == Role);
            if (role == null || !(user is IGuildUser guildUser))
            {
                return;
            }

            if (guildUser.RoleIds.Contains(role.Id))
            {
                await guildUser.RemoveRoleAsync(role);
            }
            else
            {
                await guildUser.AddRoleAsync(role);
                var channel = await Context.Guild.GetChannelAsync(PrayerChannel);
                if (channel is IMessageChannel messageChannel)
                {
                    await MessageService.SendAsync(messageChannel, $"{guildUser.Mention} has joined the prayerers.");
                }
            }
        }
    }
}