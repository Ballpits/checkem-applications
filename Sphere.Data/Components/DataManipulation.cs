using Data.Components;

namespace Sphere.Data
{
    public class DataManipulation<T> : DataAccess_Json<T>
    {
        public DataManipulation(string path)
        {
            JsonFilePath = path;
        }

        //Create new item in database
        public void Add(T dataType)
        {
            Inventory.Add(dataType);

            Save(Inventory);
        }


        //Update item in database
        public void Update(T dataType)
        {
            //Find to do item's index in Inventory
            int index = Inventory.IndexOf(dataType);

            Inventory[index] = dataType;

            Save(Inventory);
        }


        //Remove item in database
        public void Remove(T dataType)
        {
            Inventory.Remove(dataType);

            Save(Inventory);
        }
    }
}
