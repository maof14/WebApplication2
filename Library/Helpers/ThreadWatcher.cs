using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Helpers
{
    // This is the event subscriber. 

    public class ThreadWatcher : IThreadWatcher
    {
        private Dictionary<string, bool> Threads = new Dictionary<string, bool>();

        public bool IsRunning(string cacheName)
        {
            // Check the dictionary to see if this thread is running. 
            throw new NotImplementedException();
        }
    }


}
