using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Helpers
{
    public interface IThreadWatcher
    {
        /// <summary>
        /// Is this thread running for the cacheName?
        /// </summary>
        /// <param name="cacheName"></param>
        /// <returns></returns>
        bool IsRunning(string cacheName); 
    }
}
