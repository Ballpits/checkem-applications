using System;
using System.Collections.Generic;
using System.Linq;

namespace Checkem.Models
{
    public class TagManager : Tag_DataAccess_Json<TagItem>
    {
        public TagManager()
        {
            DataBasePath = @"./Tag.json";
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


        //Create new item in database
        public void Add(TagItem data)
        {
            Save(Inventory);
        }


        //Update item in database
        public void Update(TagItem data)
        {
            //Find to do item's index in Inventory
            int index = Inventory.IndexOf(data);

            Inventory[index] = data;

            Save(Inventory);
        }


        //Remove item in database
        public void Remove(TagItem data)
        {
            Inventory.Remove(data);
            Save(Inventory);
        }
    }
}
