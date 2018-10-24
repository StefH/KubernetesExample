using System;
using System.Text.RegularExpressions;
using KubernetesExampleWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using ServiceStack.Redis;

namespace KubernetesExampleWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        // GET api/test/redisconnectionstringvalue/1
        [HttpGet("{redisConnectionString}/{id}")]
        public IActionResult Get(string redisConnectionString, int id)
        {
            try
            {
                var redisClient = new RedisClient(redisConnectionString);

                string key = id.ToString();

                bool valueFromCache = false;
                string value = redisClient.Get<string>(key);

                if (value == null)
                {
                    value = $"value:{id}";
                    redisClient.Set(key, value);
                }
                else
                {
                    valueFromCache = true;
                }

                return Ok(new Item
                {
                    FromCache = valueFromCache,
                    Value = $"test redisConnectionString ok ! : {value}"
                });
            }
            catch (Exception ex)
            {
                return Ok(new
                {
                    Id = $"{id}",
                    RedisConnectionString = redisConnectionString,
                    Exception = Regex.Replace(ex.ToString(), @"\t|[\n\r]", " ")
                });
            }
        }
    }
}