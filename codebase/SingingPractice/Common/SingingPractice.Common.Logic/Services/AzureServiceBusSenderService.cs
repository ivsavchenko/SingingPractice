using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using SingingPractice.Common.Constants;
using SingingPractice.Common.Contracts.Services;

namespace SingingPractice.Common.Logic.Services
{
    public class AzureServiceBusSenderService : IMessageSenderService
    {
        public async Task SendAsync<T>(T data) where T : class
        {
            var serialized = JsonSerializer.Serialize(data);
            var messageBody = Encoding.UTF8.GetBytes(serialized);

            var builder = new ServiceBusConnectionStringBuilder(EnvironmentConstants.ServiceBusWriterConnection)
            {
                EntityPath = EnvironmentConstants.ServiceBusEntityPath
            };

            var client = new QueueClient(builder);
            await client.SendAsync(new Message(messageBody));
        }
    }
}
