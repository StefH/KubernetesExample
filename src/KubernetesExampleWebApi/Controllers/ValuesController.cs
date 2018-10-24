using System.Collections.Generic;
using KubernetesExampleWebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace KubernetesExampleWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new[] { "no cache : value1", "no cache : value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(new Item
            {
                Value = $"no cache : {id}"
            });
        }
    }
}
