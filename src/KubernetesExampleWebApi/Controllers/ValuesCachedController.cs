using System.Collections.Generic;
using KubernetesExampleWebApi.Models;
using KubernetesExampleWebApi.Redis;
using Microsoft.AspNetCore.Mvc;

namespace KubernetesExampleWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesCachedController : ControllerBase
    {
        private readonly IRedisCache _cache;

        public ValuesCachedController(IRedisCache cache)
        {
            _cache = cache;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (id > 0)
            {
                string key = id.ToString();

                bool valueFromCache = false;
                string value = _cache.GetValue<string>(key);

                if (value == null)
                {
                    value = $"value:{id}";
                    _cache.SaveValue(key, value);
                }
                else
                {
                    valueFromCache = true;
                }

                return Ok(new Item
                {
                    FromCache = valueFromCache,
                    Value = value
                });
            }

            return Ok(new Item
            {
                Value = id.ToString()
            });
        }
    }
}
