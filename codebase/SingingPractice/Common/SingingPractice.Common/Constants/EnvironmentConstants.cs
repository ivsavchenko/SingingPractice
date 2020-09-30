using System;

namespace SingingPractice.Common.Constants
{
    public static class EnvironmentConstants
    {
        public static string AppInsightsInstrumenationKey => GetValue(ConfigurationConstants.AppInsightsInstrumentationKey);

        public static string ConnectionString => GetValue(ConfigurationConstants.ConnectionStringKey);

        public static string ServiceBusWriterConnection => GetValue(ConfigurationConstants.ServiceBusWriterConnectionKey);

        public static string ServiceBusEntityPath => GetValue(ConfigurationConstants.ServiceBusEntityPathKey);

        public static string GetValue(string key)
        {
            return Environment.GetEnvironmentVariable(key) ?? throw new NullReferenceException(key);
        }
    }
}
