using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using SingingPractice.SignatureGenerator.Common.Contracts.Services;

namespace SingingPractice.SignatureGenerator.Function
{
    public class SignatureGeneratorFunction
    {
        private readonly INotificationSender notificationSender; 

        public SignatureGeneratorFunction(INotificationSender notificationSender)
        {
            this.notificationSender = notificationSender;
        }

        [FunctionName("SignatureGenerator")]
        public void Run([ServiceBusTrigger("singing-practice", Connection = "ServiceBusReaderConnection")]string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");
        }
    }
}
