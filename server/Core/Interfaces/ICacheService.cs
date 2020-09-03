using System;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ICacheService
    {
        Task CacheResponseAsync(string cacheKey, object response, TimeSpan timeSpan);
        Task<string> GetCacheResponseAsync(string cacheKey);
    }
}