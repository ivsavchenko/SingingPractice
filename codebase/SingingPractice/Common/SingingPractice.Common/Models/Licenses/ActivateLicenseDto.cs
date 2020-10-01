using System.ComponentModel.DataAnnotations;

namespace SingingPractice.Common.Models.Licenses
{
    public class ActivateLicenseDto
    {
        [Required]
        public string Key { get; set; }

        public ActivateLicenseCustomerDto User { get; set; }
    }
}
