using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Bibby.Bot.Options;
using Bibby.Bot.Services.Translations;
using Bibby.Bot.Tests.Properties;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Bibby.Bot.Tests.Services.Translation
{
    [TestClass]
    public class TranslatorTests
    {
        [TestMethod]
        public void GetTranslatorRequestMethodTest()
        {
            var keyMock = "KeyMock";
            var optionsMock = new Mock<IOptions<AzureOptions>>();
            optionsMock.SetupGet(o => o.Value).Returns(new AzureOptions() { TranslatorKey = keyMock });

            var translator = new Translator(optionsMock.Object);
            var uri = "base";
            var actual = translator.GetTranslatorRequest(uri, null);
            Assert.AreEqual(HttpMethod.Post, actual.Method);
        }

        [TestMethod]
        public void GetTranslatorRequestUriTest()
        {
            var keyMock = "KeyMock";
            var optionsMock = new Mock<IOptions<AzureOptions>>();
            optionsMock.SetupGet(o => o.Value).Returns(new AzureOptions() { TranslatorKey = keyMock });

            var translator = new Translator(optionsMock.Object);
            var uri = "base";
            var actual = translator.GetTranslatorRequest(uri, null);
            Assert.AreEqual(uri, actual.RequestUri.ToString());
        }

        [TestMethod]
        public void GetTranslatorRequestKeyHeaderTest()
        {
            var keyMock = "KeyMock";
            var optionsMock = new Mock<IOptions<AzureOptions>>();
            optionsMock.SetupGet(o => o.Value).Returns(new AzureOptions() { TranslatorKey = keyMock });

            var translator = new Translator(optionsMock.Object);
            var uri = "base";
            var request = translator.GetTranslatorRequest(uri, null);
            var actual = request.Headers.GetValues(Translator.AzureAuthHeaderName).First();
            Assert.AreEqual(keyMock, actual);
        }

        [TestMethod]
        public void GetStringContentContentEncodingTest()
        {
            var text = "Text zum Übersetzen";
            var actual = Translator.GetStringContent(text);
            Assert.AreEqual(Encoding.UTF8.BodyName, actual.Headers.ContentType.CharSet);
        }

        [TestMethod]
        public void GetStringContentMediaTypeTest()
        {
            var text = "Text zum Übersetzen";
            var actual = Translator.GetStringContent(text);
            Assert.AreEqual("application/json", actual.Headers.ContentType.MediaType);
        }

        [TestMethod]
        public async Task GetStringContentBodyTest()
        {
            var text = "Übersetzung";
            var stringContent = Translator.GetStringContent(text);
            var actual = await stringContent.ReadAsStringAsync();
            var expected = Regex.Replace(Resources.TranslationRequest, @"\s+", string.Empty);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetTranslationFromResponseBody()
        {
            var body = Resources.TranslationResponseError;
            var actual = Translator.GetTranslationFromResponseBody(body).Error.Message;
            Assert.AreEqual("The field Text must be a string or array type with a minimum length of '1'.", actual);
        }

        [TestMethod]
        public void GetTranslationFromResponseBodyWithError()
        {
            var body = Resources.TranslationResponse;
            var actual = Translator.GetTranslationFromResponseBody(body).Translations.First().Text;
            Assert.AreEqual("Text for translation", actual);
        }

        [TestMethod]
        public async Task DetectAndTranslateAsyncWithNoKey()
        {
            var optionsMock = new Mock<IOptions<AzureOptions>>();
            optionsMock.SetupGet(o => o.Value).Returns(new AzureOptions());
            var translator = new Translator(optionsMock.Object);
            var expected = Translator.AzureKeyNotFound;
            var actual = await translator.DetectAndTranslateAsync(null, null);
            Assert.AreEqual(expected, actual.Error.Message);
        }

        [TestMethod]
        public void GetUrlWithQuerySingleTests()
        {
            var expected = "base&to=en";
            var actual = Translator.GetUrlWithQuery("base", new[] { Language.English });
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetUrlWithQueryMultipleTests()
        {
            var expected = "base&to=en&to=de&to=tlh-Qaak";
            var actual = Translator.GetUrlWithQuery("base", new[] { Language.English, Language.German, Language.KlingonPlqaD });
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetDefaultLanguageIfEmpty()
        {
            var actual = Translator.GetDefaultIfEmpty(new Language[0]);
            CollectionAssert.AreEquivalent(new[] { Language.English }, actual);
        }

        [TestMethod]
        public void GetDefaultLanguageIfEmptyNull()
        {
            var actual = Translator.GetDefaultIfEmpty(null);
            CollectionAssert.AreEquivalent(new[] { Language.English }, actual);
        }

        [TestMethod]
        public void GetDefaultLanguageIfNotEmpty()
        {
            var expected = new[] { Language.English, Language.Arabic, Language.BosnianLatin };
            var actual = Translator.GetDefaultIfEmpty(expected);
            CollectionAssert.AreEquivalent(expected, actual);
        }
    }
}
