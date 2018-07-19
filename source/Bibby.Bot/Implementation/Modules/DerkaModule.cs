using System.Threading.Tasks;
using Bibby.Bot.Services;
using Bibby.Bot.Services.Translations;
using Discord.Commands;

namespace Bibby.Bot.Modules
{
    public class DerkaModule : ModuleBase<ICommandContext>
    {
        public ITranslator Translator { get; set; }
        public IMessageService MessageService { get; set; }

        [Command("derka")]
        [Alias("d", "translate")]
        public async Task RunAsync(params string[] words)
        {
            var text = string.Join(" ", words);
            var response = await Translator.DetectAndTranslateAsync(text);
            await MessageService.SendAsync(Context.Channel, response);
        }
    }
}
