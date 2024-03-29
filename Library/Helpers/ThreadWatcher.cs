﻿using System;
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

        public bool IsRunning(string cacheName)
        {
            return Threads.ContainsKey(cacheName) && Threads[cacheName];
        }

        public void StartThread(ThreadStartedEvent threadStartedEvent)
        {
            if (Threads.ContainsKey(threadStartedEvent.Name))
                Threads[threadStartedEvent.Name] = true;
            else
                Threads.Add(threadStartedEvent.Name, true);
        }

        public void FinishThread(ThreadFinishedEvent threadFinishedEvent)
        {
            if (Threads.ContainsKey(threadFinishedEvent.Name))
                Threads[threadFinishedEvent.Name] = false;
            else
                Threads.Add(threadFinishedEvent.Name, false); // Should never ever happen, because it is never finished before initialized... 
        }
    }
}
