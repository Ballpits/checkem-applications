using ProjectSC.Models.Object.Tag;
using ProjectSC.Models.ToDo;
using System;
using System.Collections.Generic;

namespace ProjectSC.Models.Interface
{
    interface IDataAccess
    {
        void StoreTestData(List<ToDoItem> inventory);



        void RetrieveData(ref List<ToDoItem> inventory);

        void RetrieveTimeData(ref List<Object.Notification.Notifications> timeRecords);



        void ResetId(List<ToDoItem> inventory);



        void Remove(ToDoItem toDoItem, List<ToDoItem> inventory);



        void AddNew(ToDoItem toDoItem, List<ToDoItem> inventory);



        void Update(ToDoItem toDoItem, List<ToDoItem> inventory);
    }
}
