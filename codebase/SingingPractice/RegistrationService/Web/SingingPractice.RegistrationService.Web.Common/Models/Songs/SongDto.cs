using System.ComponentModel.DataAnnotations;

namespace SingingPractice.RegistrationService.Web.Common.Models.Songs
{
    public class SongDto
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public string Signature { get; set; }
    }
}
