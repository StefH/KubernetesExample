using Microsoft.Extensions.Options;
using ServiceStack.Redis;

namespace KubernetesExampleWebApi.Redis
{
    /// <summary>
    /// Based on
    /// - https://www.infoworld.com/article/3187905/application-development/how-to-work-with-redis-cache-in-net.html
    /// - https://github.com/garywoodfine/redis-mvc-core
    /// </summary>
    internal class RedisCache : IRedisCache
    {
        private readonly RedisClient _redisClient;

        public RedisCache(IOptions<RedisConfiguration> configuration)
        {
            _redisClient = new RedisClient(configuration.Value.Host);
        }

        public T GetValue<T>(string key)
        {
            return _redisClient.Get<T>(key);
        }

        public bool SaveValue<T>(string key, T value)
        {
            bool isSuccess = false;
            if (_redisClient.Get<T>(key) == null)
            {
                isSuccess = _redisClient.Set(key, value);
            }

            return isSuccess;
        }
    }
}
