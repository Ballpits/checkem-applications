using Cyclops.Models.Objects;

namespace Cyclops.Models.Interface
{
    interface IDataAccess
    {
        void StoreTestData();


        void ResetId();


        public void Create(ToDoItem toDoItem);


        void Retrieve();


        //void RetrieveReminderDate();


        void Update(ToDoItem toDoItem);


        void Remove(ToDoItem toDoItem);
    }
}
