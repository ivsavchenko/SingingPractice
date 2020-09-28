using System;

namespace SingingPractice.Common.Constants
{
    public static class EnvironmentConstants
    {
        public static string AppInsightsInstrumenationKey =
            Environment.GetEnvironmentVariable(ConfigurationConstants.AppInsightsInstrumenationKey) ??
            throw new NullReferenceException(nameof(ConfigurationConstants.AppInsightsInstrumenationKey));
    }
}
