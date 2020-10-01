using System.ComponentModel.DataAnnotations;

namespace SingingPractice.Common.Models.Licenses
{
    public class ActivateLicenseCustomerDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string Address { get; set; }
    }
}
