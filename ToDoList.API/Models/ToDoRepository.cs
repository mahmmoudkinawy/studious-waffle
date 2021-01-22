using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.API.Models.DatabaseContext;

namespace ToDoList.API.Models
{
    public class ToDoRepository : IToDoRepository
    {
        private readonly ToDoDbContext _context;

        public ToDoRepository(ToDoDbContext context)
        {
            _context = context ??
                throw new ArgumentNullException(nameof(context));
        }

        public void AddItem(ToDoItem toDo)
        {
            toDo.Id = Guid.NewGuid();

            _context.ToDoItems.Add(toDo);
        }

        public void DeleteItem(ToDoItem toDo)
        {
            _context.ToDoItems.Remove(toDo);
        }

        public ToDoItem GetItem(Guid toDoId)
        {
            return _context.ToDoItems.FirstOrDefault(kh => kh.Id == toDoId);
        }

        public bool ToDoItemExits(Guid toDoId)
        {
            return _context.ToDoItems.Any(kh => kh.Id == toDoId);
        }

        public IEnumerable<ToDoItem> ToDoItems()
        {
            return from kh
                   in _context.ToDoItems
                   select kh;
        }

        public ToDoItem UpdateItem(ToDoItem toDo)
        {
            var updatedToDo = _context.ToDoItems.SingleOrDefault(kh => kh.Id == toDo.Id);
            
            if(updatedToDo != null)
            {
                updatedToDo.Text = toDo.Text;
                updatedToDo.Completed = toDo.Completed;
            }
            return toDo;
            //var entity = _context.ToDoItems.Attach(toDo);
            //entity.State = EntityState.Modified;
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

    }
}
