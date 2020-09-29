using System;

namespace SingingPractice.Common.Constants
{
    public static class EnvironmentConstants
    {
        public static string AppInsightsInstrumenationKey => GetValue(ConfigurationConstants.AppInsightsInstrumenationKey);

        public static string ConnectionString => GetValue(ConfigurationConstants.ConnectionStringKey);

        public static string GetValue(string key)
        {
            return Environment.GetEnvironmentVariable(key) ?? throw new NullReferenceException(key);
        }
    }
}
