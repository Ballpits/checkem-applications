using System.Collections.Generic;

namespace Data
{
    internal interface IDataAccess<T>
    {
        void Save(List<T> Inventory);

        List<T> Retrieve();
    }
}
