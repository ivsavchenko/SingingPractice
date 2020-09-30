using System.ComponentModel.DataAnnotations;

namespace SingingPractice.RegistrationService.Web.Common.Models.Licenses
{
    public class ActivateLicenseUserDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string Address { get; set; }
    }
}
