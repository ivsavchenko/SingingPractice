using System.Text;

namespace SingingPractice.Common.Logic.Extensions
{
    public static class ByteExtensions
    {
        public static string GetString(this byte[] bytes)
        {
            return Encoding.UTF8.GetString(bytes);
        }
    }
}
