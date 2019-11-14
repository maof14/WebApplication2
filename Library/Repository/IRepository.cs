using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace Library.Repository
{
    public interface IRepository<T>
    {
        IList<T> Get();
    }
}
