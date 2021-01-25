using System.Collections.Generic;

namespace Checkem.Models
{
    internal interface IDataAccess<T>
    {
        void Save(List<T> inventory);


        List<T> Retrieve();
    }
}
