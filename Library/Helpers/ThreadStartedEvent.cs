using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Helpers
{
    public class ThreadStartedEvent : EventArgs
    {

        public ThreadStartedEvent(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
    }
}
