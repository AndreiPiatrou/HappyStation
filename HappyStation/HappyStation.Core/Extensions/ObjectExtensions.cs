using System.Text.RegularExpressions;

using HappyStation.Core.Entities;

namespace HappyStation.Core.Extensions
{
    public static class ObjectExtensions
    {
        public static bool IsNew(this DatabaseEntity entity)
        {
            return entity.Id < 1;
        }

        /// <summary>
        /// Remove HTML from string with Regex.
        /// </summary>
        public static string StripTags(this string source)
        {
            return HtmlRegex.Replace(source, string.Empty).Replace("&nbsp;", " ");
        }

        public static string TryFindFirstImage(this string value)
        {
            var match = FindImageTagRegex.Match(value);
            return match.Success ? match.Groups["url"].Value : string.Empty;
        }

        /// <summary>
        /// Compiled regular expression for performance.
        /// </summary>
        private static readonly Regex HtmlRegex = new Regex("<.*?>", RegexOptions.Compiled);

        private static readonly Regex FindImageTagRegex = new Regex(@"<img.*?src=""(?<url>.*?)"".*?>", RegexOptions.IgnoreCase);
    }
}
