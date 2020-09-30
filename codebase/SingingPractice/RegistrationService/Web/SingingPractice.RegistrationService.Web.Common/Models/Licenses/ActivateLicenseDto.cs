using System;
using System.ComponentModel.DataAnnotations;

namespace SingingPractice.RegistrationService.Web.Common.Models.Licenses
{
    public class ActivateLicenseDto
    {
        [Required]
        public Guid Key { get; set; }

        public ActivateLicenseUserDto User { get; set; }
    }
}
