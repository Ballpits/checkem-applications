using System;
using System.Collections.Generic;

namespace ProjectSC
{
    interface IDataManipulation<T>
    {
        void StoreTestData(List<T> inventory);
        void Save(List<T> inventory);
        void LoadFullData(ref List<T> inventory);
        void Update(int id, string title, string description, DateTime beginDateTime, DateTime endDateTime, List<T> inventory);
        string FindById(int id, List<T> inventory);
        string FindByTitle(string title, List<T> inventory);
        void AddNew(string title, string description, DateTime beginDateTime, DateTime endDateTime, List<T> inventory);
        void Remove(int id, List<T> inventory);
        void ResetId(List<T> inventory);
    }
}
