using System.Collections.Generic;

namespace Sphere.Data
{
    internal interface IDataAccess<T>
    {
        void Save(List<T> inventory);


        List<T> Retrieve();
    }
}
