using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SingingPractice.SignatureGenerator.Common.Contracts.Services;

namespace SingingPractice.SignatureGenerator.Logic.Services
{
    public class FakeEmailNotificationSender : INotificationSender
    {
        private readonly ILogger<FakeEmailNotificationSender> logger;

        public FakeEmailNotificationSender(ILogger<FakeEmailNotificationSender> logger)
        {
            this.logger = logger;
        }

        public async Task SendAsync<T>(T notification) where T : class
        {
            await Task.Yield();

            var data = JsonSerializer.Serialize(notification);
            logger.LogInformation($"Fake email sender. Real email will contain next data:\n{data}");
        }
    }
}
