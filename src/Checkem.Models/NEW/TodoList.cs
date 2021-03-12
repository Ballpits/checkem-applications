using System.Collections.Generic;

namespace Checkem.Models.NEW
{
    public class TodoList
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Todo> Tasks { get; set; }
    }
}
