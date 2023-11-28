using Dispo.Shared.Caching.Interfaces;
using Dispo.Shared.Log;
using Microsoft.Extensions.Caching.Memory;

namespace Dispo.Shared.Caching
{
    public class CacheManager : ICacheManager
    {
        private readonly IMemoryCache _memoryCache;
        private readonly ILoggingService _logger;

        public CacheManager(IMemoryCache memoryCache, ILoggingService logger)
        {
            _memoryCache = memoryCache;
            _logger = logger;
        }

        /// <summary>
        /// Add a JSON to memory cache.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        /// <param name="expiration"></param>
        public void Add(string key, string obj, long expiration)
        {
            _memoryCache.TryGetValue(key, out string value);
            if (string.IsNullOrEmpty(value))
            {
                _memoryCache.GetOrCreate(key, factory =>
                {
                    factory.SlidingExpiration = TimeSpan.FromSeconds(expiration);
                    return obj;
                });
            }

            _logger.Information($"Adding object {obj} to memory cache.");
        }

        /// <summary>
        /// Get a JSON from memory cache.
        /// Note: You'll need to deserialize your object after getting it from cache.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string Get(string key)
        {
            _memoryCache.TryGetValue(key, out string value);
            return value;
        }

        /// <summary>
        /// Remove a JSON permanently from memory cache.
        /// </summary>
        /// <param name="key"></param>
        public void Remove(string key)
        {
            _logger.Information($"Removing object with key {key} from memory cache.");

            _memoryCache.Remove(key);
        }
    }
}