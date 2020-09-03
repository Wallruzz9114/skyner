using System;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Interfaces;
using StackExchange.Redis;

namespace Middleware.Services
{
    public class CacheService : ICacheService
    {
        private readonly IDatabase _database;

        public CacheService(IConnectionMultiplexer connectionMultiplexer) =>
            _database = connectionMultiplexer.GetDatabase();

        public async Task CacheResponseAsync(string cacheKey, object response, TimeSpan timeSpan)
        {
            if (response == null) return;

            var serializerOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var serializedResponse = JsonSerializer.Serialize(response, serializerOptions);

            await _database.StringSetAsync(cacheKey, serializedResponse, timeSpan);
        }

        public async Task<string> GetCacheResponseAsync(string cacheKey)
        {
            var cachedResponse = await _database.StringGetAsync(cacheKey);
            // If cached response is null
            if (cachedResponse.IsNullOrEmpty) return null;

            return cachedResponse;
        }
    }
}