using System;
using System.Collections.Generic;
using System.Text;
using Library.Repository;

namespace Library.Helpers
{
    // This is the event subscriber (listener)

    public class ThreadWatcher : IThreadWatcher
    {
        private Dictionary<string, bool> Threads = new Dictionary<string, bool>();

        public bool IsRunning(string cacheName)
        {
            // Check the dictionary to see if this thread is running. 
            throw new NotImplementedException();
        }

        public void StartThread(ThreadStartedEvent threadStartedEvent)
        {
            throw new NotImplementedException();
        }

        public void FinishThread(ThreadFinishedEvent threadFinishedEvent)
        {
            throw new NotImplementedException();
        }
    }
}
