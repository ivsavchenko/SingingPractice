using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace SingingPractice.RegistrationService.Web.Api.Controllers
{
    [Route("api/registrations")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly ILogger<RegistrationController> logger;

        public RegistrationController(ILogger<RegistrationController> logger)
        {
            this.logger = logger;
        }

#warning only for testing
        [HttpGet]
        public IActionResult HelloWorld()
        {
            logger.LogInformation($"HelloWorld web method was called at {DateTime.UtcNow:O}");
            return Ok("Hello World!");
        }
    }
}
