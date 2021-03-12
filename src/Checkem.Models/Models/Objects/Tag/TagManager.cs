using System;
using System.Collections.Generic;
using System.Linq;

namespace Checkem.Models
{
    public class TagManager : Tag_DataAccess_Json<TagItem>
    {
        public TagManager()
        {
            JsonFilePath = @"./Tag.json";
        }


        //Rearrange id according to the index in Inventory list
        #region ResetId
        public void ResetId()
        {
            foreach (var item in Inventory)
            {
                item.ID = Inventory.IndexOf(item);
            }

            Save(Inventory);
        }
        #endregion


        //Add new item and save to database
        public void Add(TagItem data)
        {
            Inventory.Add(data);

            Save(Inventory);
        }


        //Update item and save to database
        public void Update(TagItem data)
        {
            //Find to do item's index in Inventory than update
            Inventory[Inventory.IndexOf(data)] = data;

            Save(Inventory);
        }


        //Remove item and save to database
        public void Remove(TagItem data)
        {
            Inventory.Remove(data);

            Save(Inventory);
        }
    }
}
