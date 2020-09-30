using System.Threading.Tasks;

namespace SingingPractice.SignatureGenerator.Common.Contracts.Services
{
    public interface INotificationSender
    {
        Task SendAsync<T>(T notification) where T : class;
    }
}
