using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(10)
            }.RegisterPostEvictionCallback(EvictionCallback);
            CreateMemoryCacheChecker();
        }

        private void EvictionCallback(object key, object value, EvictionReason reason, object state)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"Cache with key: {key} expired, Reason: {reason}.");
            Console.ResetColor();
            Task<IList<Dog>> dbFetchTask = new Task<IList<Dog>>(() => DogService.Get());

            dbFetchTask.Start();

            dbFetchTask.ContinueWith((a) => { MemoryCache.Set(cacheName, a.Result, options: MemoryCacheEntryOptions); });
        }

        public IList<Dog> Get()
        {
            IList<Dog> result = MemoryCache.Get<IList<Dog>>(cacheName);

            if (result == null)
            {
                Console.WriteLine($"Using Service for {cacheName}.");
                result = DogService.Get();
                MemoryCache.Set(cacheName, result, options: MemoryCacheEntryOptions);
            }
            else
            {
                Console.WriteLine($"Used MemoryCache for {cacheName}.");
            }

            return result;
        }

        void CreateMemoryCacheChecker()
        {
            new Task(() =>
            {
                while (true)
                {
                    var cache = MemoryCache.Get(cacheName); // Kolla i memorycachen hela tiden, bör trigga
                    Console.WriteLine("Kollar memorycachen...");
                    Thread.Sleep(1000);
                }
            }).Start(TaskScheduler.Default);
        }
    }
}
