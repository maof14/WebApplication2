using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Library.Repository;

namespace Library.Helpers
{
    // This is the event subscriber (listener)

    public class ThreadWatcher : IThreadWatcher
    {
        private Dictionary<string, bool> Threads = new Dictionary<string, bool>();

        private object _lock = new object();

        public bool IsRunning(string cacheName)
        {
            lock (_lock)
            {
                return Threads.ContainsKey(cacheName) && Threads[cacheName];
            }
        }

        public void OnStartThread(object sender, ThreadStartedEvent args)
        {
            lock (_lock)
            {
                if (Threads.ContainsKey(args.Name))
                    Threads[args.Name] = true;
                else
                    Threads.Add(args.Name, true);
            }
        }

        public void OnFinishThread(object sender, ThreadFinishedEvent args)
        {
            lock (_lock)
            {
                if (Threads.ContainsKey(args.Name))
                    Threads[args.Name] = false;
                else
                    Threads.Add(args.Name, false); // Should never ever happen, because it is never finished before initialized... 
            }
        }
    }
}
