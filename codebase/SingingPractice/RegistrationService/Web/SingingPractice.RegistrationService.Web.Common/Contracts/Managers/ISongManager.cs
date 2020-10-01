using System.Threading.Tasks;
using SingingPractice.RegistrationService.Web.Common.Models.Songs;

namespace SingingPractice.RegistrationService.Web.Common.Contracts.Managers
{
    public interface ISongManager
    {
        Task<string> SingAsync(SongDto dto);
    }
}
