using System.Threading.Tasks;

namespace SingingPractice.Common.Contracts.Services
{
    public interface IMessageSenderService
    {
        Task SendAsync<T>(T data) where  T: class;
    }
}
