using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace KubernetesExampleWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ConfigController(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        // GET api/config
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(new
            {
                Secret = _configuration.GetValue<string>("secret")
            });
        }
    }
}
