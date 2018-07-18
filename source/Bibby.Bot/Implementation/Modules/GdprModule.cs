using System.Threading.Tasks;
using Bibby.Bot.Utilities.Extensions;
using Discord.Commands;

namespace Bibby.Bot.Services
{
    public class GdprModule : ModuleBase<ICommandContext>
    {
        public IMessageService MessageService { get; set; }

        [Command("gdpr")]
        public async Task RunAsync()
        {
            await MessageService.SendAsync(Context.Channel, "I ain't saving shit.").DeleteAfterSeconds(15);
        }
    }
}
