using System;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace App.Data.Helpers
{
    public static class StringExtensions
    {
        public static string Sanitize(this string stringValue)
        {
            if (null == stringValue)
                return string.Empty;
            return stringValue
                        .RegexReplace("-{2,}", "-")                 // transforms multiple --- in - use to comment in sql scripts
                        .RegexReplace(@"[*/]+", string.Empty)      // removes / and * used also to comment in sql scripts
                        .RegexReplace(@"(;|\s)(exec|execute|select|insert|update|delete|create|alter|drop|rename|truncate|backup|restore)\s", string.Empty, RegexOptions.IgnoreCase);
        }


        private static string RegexReplace(this string stringValue, string matchPattern, string toReplaceWith)
        {
            return Regex.Replace(stringValue, matchPattern, toReplaceWith);
        }

        private static string RegexReplace(this string stringValue, string matchPattern, string toReplaceWith, RegexOptions regexOptions)
        {
            return Regex.Replace(stringValue, matchPattern, toReplaceWith, regexOptions);
        }

        public static string Empty(this string stringValue)
        {
            return string.IsNullOrEmpty(stringValue) ? string.Empty : stringValue;
        }
        public static string[] SplitLines(this string stringValue)
        {
            return stringValue.Empty().Split(new[] {"\r\n", "\n"}, StringSplitOptions.None);
        }

        public static string MakeValidFileName(this string name)
        {
            var invalidChars = Regex.Escape(new string(Path.GetInvalidFileNameChars()));
            var invalidReStr = string.Format(@"[{0}]+", invalidChars);
            var s = Regex.Replace(name, invalidReStr, "_");
            return s.Replace(" ", "_")
                .Replace("%", "_");
        }
        public static string Slugify(this string phrase)
        {
            var str = phrase.RemoveAccent().ToLower();

            str = Regex.Replace(str, @"[^a-z0-9\s-]", ""); // invalid chars          
            str = Regex.Replace(str, @"\s+", " ").Trim(); // convert multiple spaces into one space  
            str = str.Substring(0, str.Length <= 250 ? str.Length : 250).Trim(); // cut and trim it  
            str = Regex.Replace(str, @"\s", "-"); // hyphens  
            str = Regex.Replace(str, @"\055+", "-").Trim('-');
            return str;
        }

        public static string RemoveAccent(this string txt)
        {
            var bytes = Encoding.GetEncoding("Cyrillic").GetBytes(txt);
            return Encoding.ASCII.GetString(bytes);
        }


        public static string Gravatar(this string email)
        {
            email = string.IsNullOrEmpty(email) ? "" : email;
            var hash = email.Trim().ToLower().Md5();
            return "http://www.gravatar.com/avatar/" + hash;
        }
        public static string Null2Empty(this string s)
        {
            if (string.IsNullOrEmpty(s))
                return string.Empty;
            return s;
        }

        public static byte[] Compress(this string value)
        {
            //Transform string into byte[]  
            var byteArray = new byte[value.Length];
            var index = 0;
            foreach (char item in value)
            {
                byteArray[index++] = (byte)item;
            }

            //Prepare for compress
            using (var ms = new MemoryStream())
            {
                using (var sw = new GZipStream(ms, CompressionMode.Compress))
                {

                    //Compress
                    sw.Write(byteArray, 0, byteArray.Length);
                    //Close, DO NOT FLUSH cause bytes will go missing...
                }
                //Transform byte[] zip data to string
                byteArray = ms.ToArray();
            }
            return byteArray;
        }

        public static string Decompress(byte[] gzBuffer)
        {
            string s;
            using (var ms = new MemoryStream())
            {
                var msgLength = BitConverter.ToInt32(gzBuffer, 0);
                ms.Write(gzBuffer, 4, gzBuffer.Length - 4);

                var buffer = new byte[msgLength];

                ms.Position = 0;
                using (var zip = new GZipStream(ms, CompressionMode.Decompress))
                {
                    zip.Read(buffer, 0, buffer.Length);
                }

                s = Encoding.UTF8.GetString(buffer);
            }
            return s;
        }
        public static string GenerateFilenameSlug(this string fileName)
        {
            var fileExtension = Path.GetExtension(fileName);
            if (fileExtension != null)
            {
                fileName = fileName.Replace(fileExtension, "").Slugify();
                return fileName + fileExtension;
            }
            return fileName.Slugify();
        }



        public static string Md5(this string password)
        {
            var textBytes = Encoding.Default.GetBytes(password);
            var cryptHandler = new MD5CryptoServiceProvider();
            var hash = cryptHandler.ComputeHash(textBytes);
            return hash.Aggregate("", (current, a) => current + a.ToString("x2"));
        }

        public static string ToUpperFirstLetter(this string source)
        {
            if (string.IsNullOrEmpty(source))
                return string.Empty;
            // convert to char array of the string
            char[] letters = source.ToCharArray();
            // upper case the first char
            letters[0] = char.ToUpper(letters[0]);
            // return the array made of the new char array
            return new string(letters);
        }

        public static DateTime ParseDate(this string s)
        {
            DateTime dt;
            DateTime.TryParseExact(s, "dd/MM/yyyy", null, DateTimeStyles.None, out dt);
            return dt;
        }
        public static DateTime ParseDateTime(this string s)
        {
            DateTime dt;
            DateTime.TryParseExact(s, "dd/MM/yyyy HH:mm", null, DateTimeStyles.None, out dt);
            return dt;
        }

    }

}
