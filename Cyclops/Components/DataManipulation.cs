using Data.Components;

namespace Checkem.Models
{
    public class DataManipulation<T> : DataAccess_Json<T>
    {
        //Create new to do item
        public void Add(T dataType)
        {
            Inventory.Add(dataType);

            Save(Inventory);
        }


        //Update to do item
        public void Update(T dataType)
        {
            //Find to do item's index in Inventory list with it's ID
            int index = Inventory.IndexOf(dataType);

            Inventory[index] = dataType;

            Save(Inventory);
        }


        //Remove to do item
        public void Remove(T dataType)
        {
            Inventory.Remove(dataType);

            Save(Inventory);
        }
    }
}
