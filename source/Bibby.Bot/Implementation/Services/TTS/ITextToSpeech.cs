using System.IO;
using System.Threading.Tasks;

namespace Bibby.Bot.Services.TTS
{
    public interface ITextToSpeech
    {
        Task<Stream> Say(string text, TtsLanguage ttsLanguage);
    }
}