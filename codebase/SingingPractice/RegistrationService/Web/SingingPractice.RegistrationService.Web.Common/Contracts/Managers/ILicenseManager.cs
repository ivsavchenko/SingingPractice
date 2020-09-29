using System;
using System.Threading.Tasks;

namespace SingingPractice.RegistrationService.Web.Common.Contracts.Managers
{
    public interface ILicenseManager
    {
        Task<Guid> IssueAsync();

        Task ActivateAsync();
    }
}
