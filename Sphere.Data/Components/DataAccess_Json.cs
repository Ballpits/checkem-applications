using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;


namespace Data.Components
{
    public class DataAccess_Json<T> : IDataAccess<T>
    {
        //Inventory to save to do items
        public List<T> Inventory
        {
            get => Retrieve();
        }


        //File path for inventory json file
        private string JsonFilePath = @"./Inventory.json";


        //Save everthing from inventory list to Inventory.json file
        public void Save(List<T> inventory)
        {
            File.WriteAllText(@"Inventory.json", JsonConvert.SerializeObject(inventory));
        }


        //Retrive all to do items from Inventory.json file and store them into Inventory list
        public List<T> Retrieve()
        {
            string json = File.ReadAllText(JsonFilePath);

            return JsonConvert.DeserializeObject<List<T>>(json);
        }
    }
}
