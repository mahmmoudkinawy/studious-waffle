using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoList.API.Models
{
    public interface IToDoRepository
    {
        IEnumerable<ToDoItem> ToDoItems();
        void AddItem(ToDoItem toDo);
        ToDoItem UpdateItem(ToDoItem toDo);
        void DeleteItem(ToDoItem toDo);
        bool ToDoItemExits(Guid toDoId);
        ToDoItem GetItem(Guid toDoId);
        int Save();
    }
}
