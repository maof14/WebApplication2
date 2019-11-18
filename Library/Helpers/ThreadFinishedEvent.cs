using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Helpers
{
    public class ThreadFinishedEvent
    {
        public ThreadFinishedEvent(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
    }
}
