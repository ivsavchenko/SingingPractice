using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
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
        public async Task Run([ServiceBusTrigger("singing-practice", Connection = "ServiceBusReaderConnection")]string myQueueItem)
        {
            await notificationSender.SendAsync(myQueueItem);
        }
    }
}
