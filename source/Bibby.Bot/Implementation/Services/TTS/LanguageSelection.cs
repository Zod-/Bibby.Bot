using System.Collections.Generic;
using System.Linq;
using Bibby.Bot.Options;
using Microsoft.Extensions.Options;

namespace Bibby.Bot.Services.TTS
{
    public class LanguageSelection
    {
        public LanguageSelection(IOptions<TtsLanguages> ttsLanguages)
        {
            _languages = new List<TtsLanguage>(ttsLanguages.Value.Languages);
        }

        public IEnumerable<TtsLanguage> GetLanguages(string botName)
        {
            return _languages.Where(l => CompareToBotName(l, botName));
        }

        private static List<TtsLanguage> _languages;

        public bool IsBotName(string text)
        {
            var index = _languages.FindIndex(l => l.ServiceNameMapping.Contains(text));
            return index != -1;
        }

        private bool CompareToBotName(TtsLanguage ttsLanguage, string botName)
        {
            return string.IsNullOrEmpty(botName) || ttsLanguage.ServiceNameMapping.Contains(botName);
        }
    }
}
