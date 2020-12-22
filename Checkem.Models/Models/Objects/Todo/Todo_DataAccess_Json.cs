using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;


namespace Checkem.Models
{
    public class Todo_DataAccess_Json<T> : IDataAccess<T>
    {
        public Todo_DataAccess_Json()
        {
            inventory = Retrieve();
        }


        //File path for inventory json file
        public string DataBasePath = @"./Inventory.json";


        //Inventory to save to do items
        private List<T> inventory;
        public List<T> Inventory
        {
            get
            {
                return inventory;
            }
            set
            {
                if (inventory != value)
                {
                    inventory = value;
                }
            }
        }


        //Save everthing from inventory list to Inventory.json file
        public void Save(List<T> inventory)
        {
            File.WriteAllText(DataBasePath, JsonConvert.SerializeObject(inventory));
        }


        //Retrive all to do items from Inventory.json file and store them into Inventory list
        public List<T> Retrieve()
        {
            string json = File.ReadAllText(DataBasePath);

            return JsonConvert.DeserializeObject<List<T>>(json);
        }
    }
}
