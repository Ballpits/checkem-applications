using ProjectSC.Models.Object.Notification;
using ProjectSC.Models.ToDo;
using System;
using System.Collections.Generic;

namespace ProjectSC.Models.Interface
{
    interface IDataAccess
    {
        public void StoreTestData(List<ToDoItem> inventory);



        public void RetrieveData(ref List<ToDoItem> inventory);

        public void RetrieveTimeData(ref List<TimeRecord> timeRecords);




        public void ResetId(List<ToDoItem> inventory);




        public void RemoveAt(int id, List<ToDoItem> inventory);




        #region AddNew
        public void AddNew(string title, string description, DateTime begineDateTime, DateTime endDateTime, DateTime createdDateTime, List<ToDoItem> inventory);

        public void AddNew(string title, string description, DateTime endDateTime, DateTime createdDateTime, List<ToDoItem> inventory);

        public void AddNew(string title, string description, DateTime createdDateTime, List<ToDoItem> inventory);

        public void Update(int id, string title, string description, DateTime endDateTime, List<ToDoItem> inventory);

        public void Update(int id, string title, string description, DateTime beginDateTime, DateTime endDateTime, List<ToDoItem> inventory);
        #endregion



        #region Update
        public void Update(int id, string title, string description, List<ToDoItem> inventory);

        public void Update(int id, bool isStarred, List<ToDoItem> inventory);

        public void UpdateCompletion(int id, bool isCompleted, List<ToDoItem> inventory);

        public void Update(int id, string tagName, List<ToDoItem> inventory);
        #endregion
    }
}
