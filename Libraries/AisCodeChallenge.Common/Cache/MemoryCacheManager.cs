using System;
using System.Runtime.Caching;

namespace AisCodeChallenge.Common.Cache
{
    public class MemoryCacheManager : IMemoryCache

    {

        /// <summary>
        /// Cache object
        /// </summary>
        protected ObjectCache Cache => MemoryCache.Default;

        /// <summary>
        /// Gets or sets the value associated with the specified key.
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="key">The key of the value to get.</param>
        /// <returns>The value associated with the specified key.</returns>
        public virtual T Get<T>(string key)
        {
            return (T)Cache[key];
        }



        /// <summary>
        /// Adds the specified key and object to the cache.
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="data">Data</param>
        public void Set(string key, object data)
        {
            if (data == null)
                return;
            var policy = new CacheItemPolicy
            {
                AbsoluteExpiration = DateTime.Now + TimeSpan.FromMinutes(600000)
            };
            Cache.Add(new CacheItem(key, data), policy);
        }



        /// <summary>
        /// Gets a value indicating whether the value associated with the specified key is cached
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>Result</returns>
        public virtual bool IsSet(string key)
        {
            return Cache.Contains(key);
        }

        /// <summary>
        /// Removes the value with the specified key from the cache
        /// </summary>
        /// <param name="key">/key</param>
        public virtual void Remove(string key)
        {
            Cache.Remove(key);
        }



        /// <summary>
        /// Clear all cache data
        /// </summary>
        public virtual void Clear()
        {
            foreach (var item in Cache)
                Remove(item.Key);
        }


        /// <summary>
        /// Dispose
        /// </summary>
        public virtual void Dispose()
        {


        }

    }
}
