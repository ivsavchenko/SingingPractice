using System;

namespace SingingPractice.RegistrationService.Web.Common.Models.Licenses
{
    public class ActivateLicenseDto
    {
        public Guid Key { get; set; }

        public ActivateLicenseUserDto User { get; set; }
    }
}
