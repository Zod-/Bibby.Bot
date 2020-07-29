using System.Linq;
using System.Threading.Tasks;
using Bibby.Bot.Services;
using Bibby.Bot.Services.Translations;
using Discord.Commands;
using JetBrains.Annotations;

namespace Bibby.Bot.Commands
{
    [UsedImplicitly]
    public class Derka : ModuleBase<ICommandContext>
    {
        public ITranslator Translator { get; set; }
        public IMessageService MessageService { get; set; }

        [Command("derka")]
        [Alias("d", "translate")]
        public async Task RunAsync(params string[] words)
        {
            var text = string.Join(" ", words);
            var response = await Translator.DetectAndTranslateAsync(text);
            if (response.Error != null)
            {
                await RespondWithError(response);
            }
            else
            {
                await ResponsWithTranslation(response);
            }
        }

        private async Task ResponsWithTranslation(TranslationResponse response)
        {
            var text = response.Translations.First().Text;
            await MessageService.SendAsync(Context.Channel, text);
        }

        private async Task RespondWithError(TranslationResponse response)
        {
            var errorText = $"**Error**: {response.Error.Message}";
            var deletingProgress = "\nDeleting message and command";
            var message = await MessageService.SendAsync(Context.Channel, errorText + deletingProgress);
            for (var i = 0; i < 5; i++)
            {
                deletingProgress += ".";
                var text = errorText + deletingProgress;
                await message.ModifyAsync(p => p.Content = text);
                await Task.Delay(1000);
            }

            var deleteMessage = message.DeleteAsync();
            var deleteContext = Context.Message.DeleteAsync();
            Task.WaitAll(deleteMessage, deleteContext);
        }
    }
}
