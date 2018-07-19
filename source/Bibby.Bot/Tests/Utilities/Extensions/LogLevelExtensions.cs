using Bibby.Bot.Utilities.Extensions;
using Discord;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bibby.Bot.Tests.Utilities.Extensions
{
    [TestClass]
    public class LogLevelExtensions
    {
        [TestMethod]
        [DataRow(LogLevel.Error, LogSeverity.Error)]
        [DataRow(LogLevel.Critical, LogSeverity.Critical)]
        [DataRow(LogLevel.Trace, LogSeverity.Verbose)]
        [DataRow(LogLevel.Information, LogSeverity.Info)]
        [DataRow(LogLevel.Warning, LogSeverity.Warning)]
        [DataRow(LogLevel.Debug, LogSeverity.Debug)]
        public void ConvertLoglevelTests(LogLevel expected, LogSeverity input)
        {
            var actual = input.ToLogLevel();
            Assert.AreEqual(expected, actual);
        }
    }
}
