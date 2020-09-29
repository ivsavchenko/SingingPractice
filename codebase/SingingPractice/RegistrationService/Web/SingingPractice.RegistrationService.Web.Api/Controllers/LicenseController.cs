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

        /// <summary>
        /// It's for testing needs only
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("issue")]
        public async Task<IActionResult> IssueAsync()
        {
            var key = await licenseManager.IssueAsync();
            return Ok(key);
        }

        [HttpPost]
        [Route("validate")]
        public async Task<IActionResult> ValidateAsync([FromBody]string key)
        {
            var status = await licenseManager.ValidateAsync(key);
            return Ok(status);
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