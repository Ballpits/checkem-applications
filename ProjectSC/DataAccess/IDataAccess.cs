using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSC.DataAccess
{
    interface IDataAccess
    {
        public void StoreTestData(List<ToDoItem> inventory);

        public void RetrieveData(ref List<ToDoItem> inventory, string path);
    }
}
