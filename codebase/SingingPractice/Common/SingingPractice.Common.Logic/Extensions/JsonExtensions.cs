using System.Text.Json;

namespace SingingPractice.Common.Logic.Extensions
{
    public static class JsonExtensions
    {
        public static string ToJsonBase64<T>(this T instance)
        {
            var serialized = JsonSerializer.Serialize(instance);
            var result = serialized.ToBase64();
            return result;
        }

        public static T FromJsonBase64<T>(this string s)
        {
            var json = s.FromBase64();
            var deserialized = JsonSerializer.Deserialize<T>(json);
            return deserialized;
        }
    }
}
