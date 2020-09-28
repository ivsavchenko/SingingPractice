using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace SingingPractice.RegistrationService.Web.Api.Controllers
{
    [Route("api/registrations")]
    [ApiController]
    [AllowAnonymous]
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
            logger.LogInformation($"Hello World web method was called at {DateTime.UtcNow:O}");
            return Ok("Hello World!");
        }
    }
}
