using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SingingPractice.RegistrationService.Web.Common.Contracts.Managers;

namespace SingingPractice.RegistrationService.Web.Api.Controllers
{
    [AllowAnonymous]
    [Route("api/licenses")]
    [ApiController]
    public class LicenseController : ControllerBase
    {
        private readonly ILicenseManager licenseManager;

        public LicenseController(ILicenseManager licenseManager)
        {
            this.licenseManager = licenseManager;
        }

        [HttpPost]
        [Route("issue")]
        public async Task<IActionResult> IssueAsync()
        {
            var key = await licenseManager.IssueAsync();
            return Ok(key);
        }
    }
}
