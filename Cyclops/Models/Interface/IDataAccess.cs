using Cyclops.Models.Objects;
using System.Collections.Generic;

namespace Cyclops.Models.Interface
{
    internal interface IDataAccess
    {
        void StoreTestData();


        void ResetId();


        void Add(ToDoItem toDoItem);


        List<ToDoItem> Retrieve();


        //void RetrieveReminderDate();


        void Update(ToDoItem toDoItem);


        void Remove(ToDoItem toDoItem);
    }
}
