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
        internal const string AzureKeyNotFound = "Azure text translator key not found.";
        internal const string AzureAuthHeaderName = "Ocp-Apim-Subscription-Key";
        private readonly AzureOptions _azureKeys;

        public Translator(IOptions<AzureOptions> options)
        {
            _azureKeys = options.Value;
        }

        public async Task<string> DetectAndTranslateAsync(string text, params Language[] toLanguage)
        {
            if (string.IsNullOrEmpty(_azureKeys.TranslatorKey))
            {
                return await Task.FromResult(AzureKeyNotFound);
            }

            toLanguage = GetDefaultIfEmpty(toLanguage);
            var uri = GetUrlWithQuery(TranslateUrl, toLanguage);
            var responseBody = await GetResponseBodyAsync(text, uri);
            var result = GetTranslationFromResponseBody(responseBody);
            return result;
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

        internal static string GetTranslationFromResponseBody(string responseBody)
        {
            //Error comes back as a single object
            if (responseBody.StartsWith('{'))
            {
                responseBody = $"[{responseBody}]";
            }

            var result = JsonConvert.DeserializeObject<TranslationResponse[]>(responseBody).First();
            if (result.Error != null)
            {
                return result.Error.Message;
            }
            return result.Translations.First().Text;
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
