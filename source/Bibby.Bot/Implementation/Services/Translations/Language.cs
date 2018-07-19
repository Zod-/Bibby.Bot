
namespace Bibby.Bot.Services.Translations
{
    public class Language
    {

        private string _languageCode;
        private Language() { }

        public static implicit operator string(Language language)
        {
            return language._languageCode;
        }

        public static implicit operator Language(string languageCode)
        {
            return new Language
            {
                _languageCode = languageCode
            };
        }

        public static readonly Language Afrikaans = "af";
        public static readonly Language Arabic = "ar";
        public static readonly Language ArabicLevantine = "apc";
        public static readonly Language Bangla = "bn";
        public static readonly Language BosnianLatin = "bs";
        public static readonly Language Bulgarian = "bg";
        public static readonly Language CantoneseTraditional = "yue";
        public static readonly Language Catalan = "ca";
        public static readonly Language ChineseSimplified = "zh-Hans";
        public static readonly Language ChineseTraditional = "zh-Hant";
        public static readonly Language Croatian = "hr";
        public static readonly Language Czech = "cs";
        public static readonly Language Danish = "da";
        public static readonly Language Dutch = "nl";
        public static readonly Language English = "en";
        public static readonly Language Estonian = "et";
        public static readonly Language Fijian = "fj";
        public static readonly Language Filipino = "fil";
        public static readonly Language Finnish = "fi";
        public static readonly Language French = "fr";
        public static readonly Language German = "de";
        public static readonly Language Greek = "el";
        public static readonly Language HaitianCreole = "ht";
        public static readonly Language Hebrew = "he";
        public static readonly Language Hindi = "hi";
        public static readonly Language HmongDaw = "mww";
        public static readonly Language Hungarian = "hu";
        public static readonly Language Icelandic = "is";
        public static readonly Language Indonesian = "id";
        public static readonly Language Italian = "it";
        public static readonly Language Japanese = "ja";
        public static readonly Language Kiswahili = "sw";
        public static readonly Language Klingon = "tlh";
        public static readonly Language KlingonPlqaD = "tlh-Qaak";
        public static readonly Language Korean = "ko";
        public static readonly Language Latvian = "lv";
        public static readonly Language Lithuanian = "lt";
        public static readonly Language Malagasy = "mg";
        public static readonly Language Malay = "ms";
        public static readonly Language Maltese = "mt";
        public static readonly Language Norwegian = "nb";
        public static readonly Language Persian = "fa";
        public static readonly Language Polish = "pl";
        public static readonly Language Portuguese = "pt";
        public static readonly Language QueretaroOtomi = "otq";
        public static readonly Language Romanian = "ro";
        public static readonly Language Russian = "ru";
        public static readonly Language Samoan = "sm";
        public static readonly Language SerbianCyrillic = "sr-Cyrl";
        public static readonly Language SerbianLatin = "sr-Latn";
        public static readonly Language Slovak = "sk";
        public static readonly Language Slovenian = "sl";
        public static readonly Language Spanish = "es";
        public static readonly Language Swedish = "sv";
        public static readonly Language Tahitian = "ty";
        public static readonly Language Tamil = "ta";
        public static readonly Language Thai = "th";
        public static readonly Language Tongan = "to";
        public static readonly Language Turkish = "tr";
        public static readonly Language Ukrainian = "uk";
        public static readonly Language Urdu = "ur";
        public static readonly Language Vietnamese = "vi";
        public static readonly Language Welsh = "cy";
        public static readonly Language YucatecMaya = "yua";
    }
}
