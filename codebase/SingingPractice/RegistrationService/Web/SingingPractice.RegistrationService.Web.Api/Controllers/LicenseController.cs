using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SingingPractice.Common.Models.Licenses;
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

        /// <summary>
        /// Issues license key for demonstration needs. In the real world application this method mustn't be public
        /// </summary>
        /// <returns>License key</returns>
        [HttpPost]
        [Route("issue")]
        public async Task<IActionResult> IssueAsync()
        {
            var key = await licenseManager.IssueAsync();
            return Ok(key);
        }

        /// <summary>
        /// Validate issued license key.
        /// </summary>
        /// <param name="key">License key to validate</param>
        /// <returns>One of the statuses of the license key: Invalid, Inactive, Active</returns>
        [HttpPost]
        [Route("validate")]
        public async Task<IActionResult> ValidateAsync([FromBody]string key)
        {
            var status = await licenseManager.ValidateAsync(key);
            return Ok(status);
        }

        /// <summary>
        /// Activate issued license key and bind it to a given user
        /// </summary>
        /// <example>Example of input data: {"Key":"eyJJZCI6ImY4ZjMxY2ZhLTNlZTItNDAwYy05ZjQxLTdmMDE0NzM5NDg3NiIsIktleSI6ImE3ZmYwMWI5LTFhOTItNDllZC1iOTc1LTQ3MzVjMzIzYmQ0MSJ9","User":{"Name":"Qwerty","Email":"m@m.m","Address":"some address"}}</example>
        [HttpPost]
        [Route("activate")]
        public async Task<IActionResult> ActivateAsync([FromBody]ActivateLicenseDto dto)
        {
            await licenseManager.ActivateAsync(dto);
            return Ok();
        }
    }
}