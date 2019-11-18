using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Services
{
    public interface IHumanService
    {
        IList<Human> Get();
    }

    public class Human
    {
        public string Name { get; set; }
        public string Occupation { get; set; }
        public string Location { get; set; }
    }
}
