using System.Threading.Tasks;
using SingingPractice.Common.Models.Licenses;
using SingingPractice.RegistrationService.Web.Common.Enums;

namespace SingingPractice.RegistrationService.Web.Common.Contracts.Managers
{
    public interface ILicenseManager
    {
        Task<string> IssueAsync();

        Task ActivateAsync(ActivateLicenseDto dto);

        Task<LicenseStatus> ValidateAsync(string key);
    }
}
