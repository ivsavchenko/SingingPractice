using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SingingPractice.RegistrationService.Web.Common.Contracts.Managers;
using SingingPractice.RegistrationService.Web.Common.Models.Licenses;

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

        [HttpPost]
        [Route("validate/{key}")]
        public async Task<IActionResult> ValidateAsync(Guid key)
        {
            var status = await licenseManager.ValidateAsync(key);
            return Ok();
        }

        [HttpPost]
        [Route("activate")]
        public async Task<IActionResult> ActivateAsync([FromBody]ActivateLicenseDto dto)
        {
            await licenseManager.ActivateAsync(dto);
            return Ok();
        }
    }
}