using System;

namespace AisCodeChallenge.Common.Cache
{

    /// <summary>
    /// Extensions
    /// </summary>
    public static class CacheExtensions
    {
        public static T Get<T>(this IMemoryCache memoryCache, string key, Func<T> acquire)
        {
            return Get(memoryCache, key, 600000, acquire);
        }

        /// <summary>
        /// Get a cached item. If it's not in the cache yet, then load and cache it
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="cacheManager">Cache manager</param>
        /// <param name="key">Cache key</param>
        /// <param name="cacheTime">Cache time in minutes (0 - do not cache)</param>
        /// <param name="acquire">Function to load item if it's not in the cache yet</param>
        /// <returns>Cached item</returns>
        public static T Get<T>(this IMemoryCache memoryCache, string key, int cacheTime, Func<T> acquire)
        {
            if (memoryCache.IsSet(key))
            {
                return memoryCache.Get<T>(key);
            }

            var result = acquire();
            if (cacheTime > 0)
                memoryCache.Set(key, result);
            return result;
        }

    }
}
