using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace App.Web.Helpers
{
    public static class StringExtensions
    {
        public static string RemoveHtmlTags(this string source)
        {
            const string expn = "<.*?>";
            if (string.IsNullOrEmpty(source))
                return "";
            source = Regex.Replace(source, expn, string.Empty);
            source = HttpUtility.HtmlDecode(source);
            return source;
        }
        public static string TruncateHtml(this string input, int length)
        {
            return TruncateHtml(input, length, true);
        }
        public static string TruncateHtml(this string input, int length, bool ellipsis)
        {
            return input.RemoveHtmlTags().TruncateAtWord(length, ellipsis);
        }
        public static string Truncate(this string str, int maxLength)
        {
            return Truncate(str, maxLength, false);
        }

        public static string Truncate(this string str, int maxLength, bool ellipsis)
        {
            if (str == null) return null;
            return str.Substring(0, Math.Min(maxLength, str.Length)) + (ellipsis && maxLength < str.Length ? "..." : "");
        }
        public static string TruncateAtWord(this string input, int length)
        {
            return TruncateAtWord(input, length, true);
        }



        public static string TruncateAtWord(this string input, int length, bool ellipsis)
        {
            if (input == null || input.Length < length)
                return input;
            var iNextSpace = input.LastIndexOf(" ", length, StringComparison.Ordinal);
            return string.Format("{0}" + (ellipsis ? "..." : ""), input.Substring(0, (iNextSpace > 0) ? iNextSpace : length).Trim());
        }




    }
}