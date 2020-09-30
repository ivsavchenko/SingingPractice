using System;
using System.Text;

namespace SingingPractice.Common.Logic.Extensions
{
    public static class StringExtensions
    {
        public static byte[] GetBytes(this string s)
        {
            return Encoding.UTF8.GetBytes(s);
        }

        public static string ToBase64(this string s)
        {
            var bytes = s.GetBytes();
            return Convert.ToBase64String(bytes);
        }

        public static string FromBase64(this string s)
        {
            var bytes = Convert.FromBase64String(s);
            return bytes.GetString();
        }
    }
}
