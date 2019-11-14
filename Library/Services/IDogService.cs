using System.Collections;
using System.Collections.Generic;

namespace Library
{
    public interface IDogService
    {
        IList<Dog> Get();
    }

    public class Dog
    {
        public string Name { get; set; }
        public string Race { get; set; }
        public int Age { get; set; }
    }
}