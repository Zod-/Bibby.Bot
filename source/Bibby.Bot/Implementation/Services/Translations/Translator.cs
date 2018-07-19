using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Bibby.Bot.Options;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Bibby.Bot.Services.Translations
{
    public class Translator : ITranslator
    {
        private const string ApiUrl = "https://api.cognitive.microsofttranslator.com";
        private const string TranslateUrl = ApiUrl + "/translate?api-version=3.0";
        internal const string AzureAuthHeaderName = "Ocp-Apim-Subscription-Key";
        internal const string AzureKeyNotFound = "Azure text translator key not found.";
        internal const string TranslationSameAsInputError = "Translation is the same as the input.";
        private readonly AzureOptions _azureKeys;

        public Translator(IOptions<AzureOptions> options)
        {
            _azureKeys = options.Value;
        }

        public async Task<TranslationResponse> DetectAndTranslateAsync(string text, params Language[] toLanguage)
        {
            if (string.IsNullOrEmpty(_azureKeys.TranslatorKey))
            {
                return await Task.FromResult(GetAzureKeyNotFoundError());
            }

            toLanguage = GetDefaultIfEmpty(toLanguage);
            var uri = GetUrlWithQuery(TranslateUrl, toLanguage);
            var responseBody = await GetResponseBodyAsync(text, uri);
            var result = GetTranslationFromResponseBody(responseBody);
            if (IsSameAsInput(result, text))
            {
                result.Error = GetSameAsInputError();
            }
            return result;
        }

        private static TranslationError GetSameAsInputError()
        {
            return new TranslationError
            {
                Message = TranslationSameAsInputError
            };
        }

        private static bool IsSameAsInput(TranslationResponse response, string text)
        {
            return response.Translations != null && response.Translations.First().Text.Equals(text);
        }

        private static TranslationResponse GetAzureKeyNotFoundError()
        {
            return new TranslationResponse
            {
                Error = new TranslationError
                {
                    Message = AzureKeyNotFound
                }
            };
        }

        private async Task<string> GetResponseBodyAsync(string text, string uri)
        {
            using (var client = new HttpClient())
            using (var request = GetTranslatorRequest(uri, text))
            {
                var response = await client.SendAsync(request);
                return await response.Content.ReadAsStringAsync();
            }
        }

        internal static TranslationResponse GetTranslationFromResponseBody(string responseBody)
        {
            //Error comes back as a single object
            if (responseBody.StartsWith('{'))
            {
                responseBody = $"[{responseBody}]";
            }

            var result = JsonConvert.DeserializeObject<TranslationResponse[]>(responseBody).First();
            return result;
        }

        internal HttpRequestMessage GetTranslatorRequest(string uri, string text)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, uri)
            {
                Content = GetStringContent(text)
            };
            request.Headers.Add(AzureAuthHeaderName, _azureKeys.TranslatorKey);
            return request;
        }

        internal static StringContent GetStringContent(string text)
        {
            var content = new TranslationRequest
            {
                Text = text
            };
            var wrappedContent = new object[] { content };
            var body = JsonConvert.SerializeObject(wrappedContent);
            return new StringContent(body, Encoding.UTF8, "application/json");
        }

        internal static string GetUrlWithQuery(string baseUrl, IEnumerable<Language> toLanguage)
        {
            return $"{baseUrl}&to={string.Join("&to=", toLanguage.Select(t => (string)t))}";
        }

        internal static Language[] GetDefaultIfEmpty(Language[] toLanguage)
        {
            return toLanguage == null || toLanguage.Length == 0 ? new[] { Language.English } : toLanguage;
        }
    }
}
