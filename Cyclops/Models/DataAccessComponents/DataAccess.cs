using Cyclops.Models.Objects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cyclops.Models.DataAccessComponents
{
    public class DataAccess : DataAccess_Json
    {
        public List<ToDoItem> GetInventory()
        {
            Retrieve();
            return Inventory;
        }

        //Find all to do items
        public List<ToDoItem> Find(string searchString)
        {
            //Ignore casing
            return Inventory.FindAll(x => x.Title.ToLower().Contains(searchString.ToLower()));
        }


        //Filter do to items
        public List<ToDoItem> Filter(int mode)
        {
            /*Modes:
             * 0: return all items in inventory list
             * 1: return planned item in inventory list
             * 2: return starred item in inventory list
             * 3: return completed item in inventory list
             */

            switch (mode)
            {
                case 0:
                    {
                        return Inventory;
                    }

                case 1:
                    {
                        List<ToDoItem> list = new List<ToDoItem>();

                        foreach (var item in Inventory)
                        {
                            if (item.IsReminderOn)
                            {
                                list.Add(item);
                            }
                        }

                        return list;
                    }

                case 2:
                    {
                        List<ToDoItem> list = new List<ToDoItem>();

                        foreach (var item in Inventory)
                        {
                            if (item.IsStarred)
                            {
                                list.Add(item);
                            }
                        }

                        return list;
                    }

                case 3:
                    {
                        List<ToDoItem> list = new List<ToDoItem>();

                        foreach (var item in Inventory)
                        {
                            if (item.IsCompleted)
                            {
                                list.Add(item);
                            }
                        }

                        return list;
                    }

                default:
                    {
                        return null;
                    }

            }
        }


        //Sort and return inventory list
        public List<ToDoItem> Sort(int Mode)
        {
            /*Modes:
             * 0: return inventory ordered by ID
             * 1: return inventory ordered by ID reversed
             * 2: return inventory ordered by Importance(starred first)
             * 3: return inventory ordered by alphabetical ascending
             * 4: return inventory ordered by alphabetical descending
             * 5: return inventory ordered by begin time
             * 6: return inventory ordered by end time
             * 7: return inventory ordered by creation date
             */

            switch (Mode)
            {
                case 0:
                    {
                        return Inventory.OrderBy(x => x.ID).ToList();
                    }

                case 1:
                    {
                        return Inventory.OrderBy(x => x.ID).Reverse().ToList();
                    }

                case 2:
                    {
                        return Inventory.OrderBy(x => x.IsStarred).Reverse().ToList();
                    }

                case 3:
                    {
                        return Inventory.OrderBy(x => x.Title).ToList();
                    }

                case 4:
                    {
                        return Inventory.OrderBy(x => x.Title).Reverse().ToList();
                    }

                case 5:
                    {
                        return Inventory.OrderBy(x => x.BeginDateTime).ToList();
                    }

                case 6:
                    {
                        return Inventory.OrderBy(x => x.EndDateTime).ToList();
                    }

                case 7:
                    {
                        return Inventory.OrderBy(x => x.CreationDateTime).ToList();
                    }

                default:
                    {
                        return null;
                    }
            }
        }
    }
}
