using JetBrains.Annotations;

namespace Bibby.Bot.Services.TTS
{
    [UsedImplicitly]
    public class TtsLanguage
    {
        public string Locale { get; set; }
        public string Gender { get; set; }
        public string ServiceNameMapping { get; set; }
    }
}
