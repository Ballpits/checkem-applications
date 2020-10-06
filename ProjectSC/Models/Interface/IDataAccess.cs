using ProjectSC.Models.Object.Notification;
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




        void RemoveAt(int id, List<ToDoItem> inventory);




        #region AddNew
        void AddNew(string title, string description, DateTime begineDateTime, DateTime endDateTime, DateTime createdDateTime, List<ToDoItem> inventory);

        void AddNew(string title, string description, DateTime endDateTime, DateTime createdDateTime, List<ToDoItem> inventory);

        void AddNew(string title, string description, DateTime createdDateTime, List<ToDoItem> inventory);

        void Update(int id, string title, string description, DateTime endDateTime, List<ToDoItem> inventory);

        void Update(int id, string title, string description, DateTime beginDateTime, DateTime endDateTime, List<ToDoItem> inventory);
        #endregion



        #region Update
        void Update(int id, string title, string description, List<ToDoItem> inventory);

        void Update(int id, bool isStarred, List<ToDoItem> inventory);

        void UpdateCompletion(int id, bool isCompleted, List<ToDoItem> inventory);

        void Update(int id, string tagName, List<ToDoItem> inventory);
        #endregion
    }
}
