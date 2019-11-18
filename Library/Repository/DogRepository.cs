using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Library.Helpers;
using Microsoft.Extensions.Caching.Memory;

namespace Library.Repository
{
    public class DogRepository : IRepository<Dog>
    {
        private IMemoryCache MemoryCache { get; }

        private IDogService DogService { get; }

        private IThreadWatcher ThreadWatcher { get; }

        public event EventHandler<ThreadStartedEvent> ThreadStartedEvent;
        public event EventHandler<ThreadFinishedEvent> ThreadFinishedEvent;

        private MemoryCacheEntryOptions MemoryCacheEntryOptions { get; }

        private string cacheName = "DogCache";

        public DogRepository(IMemoryCache memoryCache, IDogService dogService, IThreadWatcher threadWatcher)
        {
            MemoryCache = memoryCache;
            DogService = dogService;
            ThreadWatcher = threadWatcher;

            MemoryCacheEntryOptions = new MemoryCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(10)
            };

            ThreadStartedEvent += OnThreadStartedEvent;
            ThreadFinishedEvent += OnThreadFinishedEvent;
        }

        private void OnThreadFinishedEvent(object sender, ThreadFinishedEvent e)
        {
            Console.WriteLine("Stopped thread!!!!!!");
            ThreadWatcher.FinishThread(e);
        }

        private void OnThreadStartedEvent(object sender, ThreadStartedEvent e)
        {
            Console.WriteLine("Started thread!!!!!!");
            ThreadWatcher.StartThread(e);
        }

        public IList<Dog> Get()
        {
            IList<Dog> result = MemoryCache.Get<IList<Dog>>(cacheName);

            if (result == null)
            {
                Task<IList<Dog>> dbFetchTask = new Task<IList<Dog>>(() => DogService.Get());

                ThreadStartedEvent?.Invoke(this, new ThreadStartedEvent(cacheName)); // Fire:a event.

                if(ThreadWatcher.IsRunning(cacheName))
                    dbFetchTask.Start(); // Starta bara tråden, om det inte redan körs en sådan. 

                dbFetchTask.ContinueWith(x =>
                {
                    MemoryCache.Set(cacheName, x.Result, options: MemoryCacheEntryOptions);
                    ThreadFinishedEvent?.Invoke(this, new ThreadFinishedEvent(cacheName)); // Tala om att tråden är klar
                });
            }
            else
            {
                Console.WriteLine($"Used MemoryCache for {cacheName}.");
            }

            return result;
        }
    }
}