using Data.Components;
using System.Collections.Generic;

namespace Sphere.Data
{
    public class DataManipulation<T> : DataAccess_Json<T>
    {
        public DataManipulation(string path)
        {
            DataBasePath = path;
        }


        //Create new item in database
        public void Add(T data)
        {
            Inventory.Add(data);

            Save(Inventory);
        }


        //Update item in database
        public void Update(T data)
        {
            //Find to do item's index in Inventory
            int index = Inventory.IndexOf(data);

            Inventory[index] = data;

            Save(Inventory);
        }


        //Remove item in database
        public void Remove(T data)
        {
            Inventory.Remove(data);

            Save(Inventory);
        }
    }
}
