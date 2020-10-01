using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using SingingPractice.SignatureGenerator.Common.Contracts.Managers;

namespace SingingPractice.SignatureGenerator.Function
{
    public class SignatureGeneratorFunction
    {
        private readonly IRegistrationManager registrationManager;

        public SignatureGeneratorFunction(IRegistrationManager registrationManager)
        {
            this.registrationManager = registrationManager;
        }

        [FunctionName("SignatureGenerator")]
        public async Task Run([ServiceBusTrigger("singing-practice", Connection = "ServiceBusReaderConnection")]string json)
        {
            await registrationManager.RegisterAsync(json);
        }
    }
}
