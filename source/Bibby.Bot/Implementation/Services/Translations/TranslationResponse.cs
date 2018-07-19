using System;

namespace Bibby.Bot.Services.Translations
{
    [Serializable]
    public class TranslationResponse
    {
        public TranslationError Error { get; set; }
        public DetectedLanguage DetectedLanguage { get; set; }
        public Translation[] Translations { get; set; }
    }

    [Serializable]
    public class Translation
    {
        public string Text { get; set; }
        public Language To { get; set; }
    }

    [Serializable]
    public class DetectedLanguage
    {
        public Language Language { get; set; }
        public double Score { get; set; }
    }

    [Serializable]
    public class TranslationError
    {
        public int Code { get; set; }
        public string Message { get; set; }
    }
}
