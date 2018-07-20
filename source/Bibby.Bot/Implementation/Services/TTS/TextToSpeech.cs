using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Bibby.Bot.Options;
using Bibby.Bot.Properties;
using Microsoft.Extensions.Options;

namespace Bibby.Bot.Services.TTS
{
    public class TextToSpeech : ITextToSpeech
    {

        private const string SpeechUrl = "https://northeurope.tts.speech.microsoft.com/cognitiveservices/v1";
        private const string AccessTokenUri = "https://northeurope.api.cognitive.microsoft.com/sts/v1.0/issueToken";
        internal const string AzureAuthHeaderName = "Ocp-Apim-Subscription-Key";
        internal const string AzureKeyNotFound = "Azure text translator key not found.";
        private readonly AzureOptions _azureKeys;
        private DateTime _accessTokenRenewTime = DateTime.MinValue;
        private string _accessToken;


        public TextToSpeech(IOptions<AzureOptions> options)
        {
            _azureKeys = options.Value;
        }

        public async Task<Stream> Say(string text, TtsLanguage ttsLanguage)
        {
            //TODO Error handling
            if (string.IsNullOrEmpty(_azureKeys.SpeechKey))
            {
                return null;
            }

            if (IsTokenExpired())
            {
                await RenewAccessToken();
            }

            var uri = GetUrlWithQuery(SpeechUrl, ttsLanguage);
            return await GetAudioResponseAsync(text, uri, ttsLanguage);
        }

        private async Task RenewAccessToken()
        {
            _accessToken = await FetchToken(AccessTokenUri, _azureKeys.SpeechKey);
            _accessTokenRenewTime = DateTime.Now;
        }

        private bool IsTokenExpired()
        {
            //Token is only valid for 10 minutes
            return (DateTime.Now - _accessTokenRenewTime).TotalMinutes > 9;
        }

        private static async Task<string> FetchToken(string fetchUri, string subscriptionKey)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add(AzureAuthHeaderName, subscriptionKey);
                UriBuilder uriBuilder = new UriBuilder(fetchUri);

                var result = await client.PostAsync(uriBuilder.Uri.AbsoluteUri, null);
                return await result.Content.ReadAsStringAsync();
            }
        }

        private async Task<Stream> GetAudioResponseAsync(string text, string uri, TtsLanguage ttsLanguage)
        {
            using (var client = new HttpClient())
            using (var request = GetSpeechRequest(uri, text, ttsLanguage))
            {
                var response = await client.SendAsync(request);
                return await response.Content.ReadAsStreamAsync();
            }
        }

        internal HttpRequestMessage GetSpeechRequest(string uri, string text, TtsLanguage ttsLanguage)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, uri)
            {
                Content = GetStringContent(text, ttsLanguage)
            };
            request.Headers.Add("Authorization", $"Bearer {_accessToken}");
            request.Headers.Add("X-Microsoft-OutputFormat", "audio-24khz-160kbitrate-mono-mp3");
            request.Headers.Add("User-Agent", "Bibby.Bot");
            return request;
        }

        internal static StringContent GetStringContent(string text, TtsLanguage language)
        {
            var body = Resources.TtsTemplate.Replace("{text}", text).Replace("{voice}", language.ServiceNameMapping);
            return new StringContent(body, Encoding.UTF8, "application/ssml+xml");
        }

        internal static string GetUrlWithQuery(string baseUrl, TtsLanguage ttsLanguage)
        {
            return $"{baseUrl}?language={ttsLanguage.Locale}";
        }
    }
}
