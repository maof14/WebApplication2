using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Caching.Memory;

namespace Library.Repository
{
    public class DogRepository : IRepository<Dog>
    {
        private IMemoryCache MemoryCache { get; }
        private IDogService DogService { get;  }
        private MemoryCacheEntryOptions MemoryCacheEntryOptions { get; }


        private string cacheName = "DogCache";

        public DogRepository(IMemoryCache memoryCache, IDogService dogService)
        {
            MemoryCache = memoryCache;
            DogService = dogService;
            MemoryCacheEntryOptions = new MemoryCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30)
            }.RegisterPostEvictionCallback(EvictionCallback);
        }

        private void EvictionCallback(object key, object value, EvictionReason reason, object state)
        {
            Console.WriteLine($"Cache with key: {key} expired, Reason: {reason}.");
        }

        public IList<Dog> Get()
        {
            IList<Dog> result = MemoryCache.Get<IList<Dog>>(cacheName);

            if (result == null)
            {
                Console.WriteLine($"Using Service for {cacheName}.");
                result = DogService.Get();
                MemoryCache.Set(cacheName, result);
            }
            else
            {
                Console.WriteLine($"Used MemoryCache for {cacheName}.");
            }

            return result;
        }
    }
}
