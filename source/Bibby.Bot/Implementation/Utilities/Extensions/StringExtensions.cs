using System.IO;
using System.Linq;

namespace Bibby.Bot.Utilities.Extensions
{
    public static class StringExtensions
    {
        private static readonly string[] ImgExtensions = { "jpg", "png", "jpeg" };

        public static bool HasImageExtension(this string file)
        {
            var extension = Path.GetExtension(file).TrimStart('.');

            return ImgExtensions.Contains(extension);
        }
    }
}