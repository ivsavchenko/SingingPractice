using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SingingPractice.RegistrationService.Web.Common.Contracts.Managers;
using SingingPractice.RegistrationService.Web.Common.Models.Songs;

namespace SingingPractice.RegistrationService.Web.Api.Controllers
{
    [Route("api/song")]
    [ApiController]
    public class SongController : ControllerBase
    {
        private readonly ISongManager songManager;

        public SongController(ISongManager songManager)
        {
            this.songManager = songManager;
        }

        [HttpPost]
        [Route("sing")]
        public async Task<IActionResult> SingAsync([FromBody] SongDto song)
        {
            var result = await songManager.SingAsync(song);
            return Ok(result);
        }
    }
}
