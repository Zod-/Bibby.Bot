using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Bibby.Bot.Services.Translations
{
    public interface ITranslator
    {
        Task<TranslationResponse> DetectAndTranslateAsync(string text, [Optional]params Language[] toLanguage);
    }
}
