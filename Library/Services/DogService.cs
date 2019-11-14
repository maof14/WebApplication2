using System;
using System.Collections.Generic;

namespace Library
{
    public class DogService : IDogService
    {
        public IList<Dog> Get()
        {
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
