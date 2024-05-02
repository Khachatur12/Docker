using StackExchange.Redis;
using Newtonsoft.Json;

namespace DockerTest.Services;
public class RedisCacheService
{
    private readonly IConnectionMultiplexer _redisConnection;

    public RedisCacheService()
    {
        _redisConnection = ConnectionMultiplexer.Connect("cache, port:6379, password=admin123#1");
    }

    public async Task<bool> SetAsync(string key, object value)
    {
        var db = _redisConnection.GetDatabase();
        var json = JsonConvert.SerializeObject(value);
        return await db.StringSetAsync(key, json);
    }

    public async Task<T?> GetAsync<T>(string key)
    {
        var db = _redisConnection.GetDatabase();
        var value = await db.StringGetAsync(key);
        if (value.HasValue)
        {
            return JsonConvert.DeserializeObject<T>(value);
        }

        return default;
    }
}