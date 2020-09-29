using System;
using System.Threading.Tasks;
using SingingPractice.RegistrationService.Web.Common.Enums;
using SingingPractice.RegistrationService.Web.Common.Models.Licenses;

namespace SingingPractice.RegistrationService.Web.Common.Contracts.Managers
{
    public interface ILicenseManager
    {
        Task<Guid> IssueAsync();

        Task ActivateAsync(ActivateLicenseDto dto);

        Task<LicenseStatus> ValidateAsync(Guid key);
    }
}
