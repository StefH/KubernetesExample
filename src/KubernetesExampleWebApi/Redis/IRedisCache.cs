namespace KubernetesExampleWebApi.Redis
{
    public interface IRedisCache
    {
        bool SaveValue<T>(string key, T value);

        T GetValue<T>(string key);
    }
}