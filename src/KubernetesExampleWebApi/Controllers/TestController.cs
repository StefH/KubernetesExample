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
        // GET api/test/redisconnectionstringvalue
        [HttpGet("{redisConnectionString}")]
        public IActionResult Get(string redisConnectionString)
        {
            try
            {
                var redisClient = new RedisClient(redisConnectionString);

                return Ok(new Item
                {
                    Value = $"get a value from cache with id = '1' : {redisClient.Get<string>("1")}"
                });
            }
            catch (Exception ex)
            {
                return Ok(new
                {
                    Info = $"redisConnectionString = '{redisConnectionString}'",
                    Exception = Regex.Replace(ex.ToString(), @"\t|[\n\r]", " ")
                });
            }
        }
    }
}