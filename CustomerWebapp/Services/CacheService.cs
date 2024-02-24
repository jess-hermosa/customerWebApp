using Microsoft.Extensions.Caching.Memory;

namespace CustomerWebapp.Services
{
    internal interface ICacheService
    {
        Task<T> GetOrSetAsync<T>(string key, Func<Task<T>> valueFactory);
        void Update<T>(string key, T value);
    }

    internal class CacheService : ICacheService
    {
        private readonly IMemoryCache _memoryCache;

        public CacheService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public async Task<T> GetOrSetAsync<T>(string key, Func<Task<T>> valueFactory)
        {
            if (!_memoryCache.TryGetValue(key, out T cachedValue))
            {
                // If the value doesn't exist in cache, set it
                cachedValue = await valueFactory.Invoke();
                _memoryCache.Set(key, cachedValue);
            }

            return cachedValue;
        }

        public void Update<T>(string key, T value)
        {
            _memoryCache.Set(key, value);
        }
    }
}
