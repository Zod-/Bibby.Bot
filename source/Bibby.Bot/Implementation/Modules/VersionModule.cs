using System.Reflection;
using System.Threading.Tasks;
using Bibby.Bot.Services;
using Bibby.Bot.Utilities.Extensions;
using Discord.Commands;

namespace Bibby.Bot.Modules
{
    public class VersionModule : ModuleBase<ICommandContext>
    {
        public IMessageService MessageService { get; set; }

        [Command("version")]
        [Alias("v")]
        public async Task RunAsync()
        {
            var version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            var message = await MessageService.SendAsync(Context.Channel, version);

            var deleteContext = Context.Message.DeleteAfterSeconds(7.5);
            var deleteMessage = message.DeleteAfterSeconds(7.5);
            Task.WaitAll(deleteMessage, deleteContext);
        }
    }
}
