using System;
using System.Collections.Generic;

namespace ProjectSC
{
    public static class ListViewer
    {
        public static string ShowList(List<ToDoItem> Inventory)
        {
            string outputString = string.Empty;

            for (int i = 0; i < Inventory.Count; i++)
            {
                outputString += $" {i}\tId:{Inventory[i].Id}\tTitle:{Inventory[i].Title}{Environment.NewLine}";
            }

            return outputString;
        }
    }
}
