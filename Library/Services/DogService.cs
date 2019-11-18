using System;
using System.Collections.Generic;
using System.Threading;

namespace Library
{
    public class DogService : IDogService
    {
        public IList<Dog> Get()
        {
            Thread.Sleep(5000); // Simulate a heavy operation
            return new List<Dog>()
            {
                new Dog()
                {
                    Age = 3,
                    Name = "Kalle",
                    Race = "Golden retreiver"
                },
                new Dog()
                {
                    Age = 45,
                    Name = "Urban",
                    Race = "Bäver"
                }
            };
        }
    }
}
