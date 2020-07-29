using System.Threading.Tasks;
using Bibby.Bot.Services;
using Bibby.Bot.Utilities.Extensions;
using Discord.Commands;
using JetBrains.Annotations;

namespace Bibby.Bot.Commands
{
    [UsedImplicitly]
    public class Gdpr : ModuleBase<ICommandContext>
    {
        public IMessageService MessageService { get; set; }

        [Command("gdpr")]
        public async Task RunAsync()
        {
            var message = await MessageService.SendAsync(Context.Channel, "I ain't saving shit.");
            var deleteResponse = message.DeleteAfterSeconds(15);
            var deleteContext = Context.Message.DeleteAfterSeconds(15);
            Task.WaitAll(deleteContext, deleteResponse);
        }
    }
}
