using System;
using System.Collections.Generic;

namespace ProjectSC
{
    interface IDataManipulation<T>
    {
        void StoreTestData(List<T> inventory);
        void RetrieveData(ref List<T> inventory);
        void ResetId(List<T> inventory);



        void AddNew(string title, string description, bool canNotify, DateTime beginDateTime, DateTime endDateTime, DateTime CreatedDateTime, List<T> inventory);
        void RemoveAt(int id, List<T> inventory);
        void SaveToJson(List<T> inventory);

        void Update(int id, string title, string description, bool canNotify, DateTime beginDateTime, DateTime endDateTime, List<T> inventory);
        void Update(int id, bool isImportant, List<T> inventory);


        string FindById(int id, List<T> inventory);
        string FindByTitle(string title, List<T> inventory);
    }
}
