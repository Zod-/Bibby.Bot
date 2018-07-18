
using Bibby.Bot.Utilities.Extensions;
using Discord;
using Microsoft.Extensions.Logging;
using NUnit.Framework;

namespace Bibby.Bot.Tests.Utilities.Extensions
{
    public class LogLevelExtensions
    {
        [TestCase(LogLevel.Error, LogSeverity.Error)]
        [TestCase(LogLevel.Critical, LogSeverity.Critical)]
        [TestCase(LogLevel.Trace, LogSeverity.Verbose)]
        [TestCase(LogLevel.Information, LogSeverity.Info)]
        [TestCase(LogLevel.Warning, LogSeverity.Warning)]
        [TestCase(LogLevel.Debug, LogSeverity.Debug)]
        public void ConvertLoglevelTests(LogLevel expected, LogSeverity input)
        {
            var actual = input.ToLogLevel();
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
