using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace SingingPractice.SignatureGenerator.Function
{
    public static class SignatureGeneratorFunction
    {
        [FunctionName("SignatureGenerator")]
        public static void Run([ServiceBusTrigger("singing-practice", Connection = "ServiceBusReaderConnection")]string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");
        }
    }
}
