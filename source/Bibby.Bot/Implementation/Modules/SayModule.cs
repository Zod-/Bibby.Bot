using System;
using System.Linq;
using System.Threading.Tasks;
using Bibby.Bot.Services;
using Bibby.Bot.Services.TTS;
using Bibby.Bot.Utilities.Extensions;
using Discord.Commands;

namespace Bibby.Bot.Modules
{
    public class SayModule : ModuleBase<ICommandContext>
    {
        private readonly Random _rand;

        public SayModule()
        {
            _rand = new Random();
        }

        public ITextToSpeech TextToSpeech { get; set; }
        public IMessageService MessageService { get; set; }
        public LanguageSelection LanguageSelection { get; set; }

        [Command("say")]
        [Alias("s", "speak")]
        public async Task RunAsync(string botName, params string[] words)
        {
            var text = string.Join(" ", words);

            if (!LanguageSelection.IsBotName(botName))
            {
                text = string.Join(" ", botName, text);
                botName = "Jessa24kRUS";
            }

            var languages = LanguageSelection.GetLanguages(botName).ToList();
            var language = languages[_rand.Next(0, languages.Count)];
            var stream = await TextToSpeech.Say(text, language);
            var message = await Context.Channel.SendFileAsync(stream, $"tts.wav");
            var messageDelete = message.DeleteAfterSeconds(30);
            var contextDelete = Context.Message.DeleteAfterSeconds(30);
            Task.WaitAll(messageDelete, contextDelete);
        }
    }
}
