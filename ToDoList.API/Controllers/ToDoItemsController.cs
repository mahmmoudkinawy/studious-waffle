using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.API.Models;

namespace ToDoList.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ToDoItemsController : ControllerBase
    {
        private readonly IToDoRepository _repository;
        private readonly IMapper _mapper;

        public ToDoItemsController(IToDoRepository repository, IMapper mapper)
        {
            _repository = repository ??
                throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public ActionResult<IEnumerable<ToDoItemDto>> GetAllToDos()
        {
            var allToDos = _repository.ToDoItems();
            if (allToDos == null) return NotFound();

            return Ok(_mapper.Map<IEnumerable<ToDoItemDto>>(allToDos));
        }

        [HttpGet("{toDoId}", Name = "GetToDoItem")]
        public IActionResult GetToDoItem(Guid toDoId)
        {
            var toDoItemFromRepo = _repository.GetItem(toDoId);
            if (toDoItemFromRepo == null) return NotFound();

            return Ok(_mapper.Map<ToDoItemDto>(toDoItemFromRepo));
        }

        [HttpPost]
        public IActionResult CreateToDo(ToDoItem toDoItem)
        {
            if (toDoItem == null) return NotFound();

            _repository.AddItem(toDoItem);
            _repository.Save();

            var toDoItemToReturn = _repository.GetItem(toDoItem.Id);

            return CreatedAtRoute("GetToDoItem", new
            {
                toDoId = toDoItemToReturn.Id
            }, toDoItemToReturn);
        }

        [HttpPut("{toDoId}")] //Fucking bug
        public IActionResult UpdateToDo(Guid toDoId)
        {
            if (toDoId == null) return NotFound();

            var toDoItemFromRepo = _repository.GetItem(toDoId);
            if (toDoItemFromRepo == null) return NotFound();

            _repository.UpdateItem(toDoItemFromRepo);
            _repository.Save();

            return NoContent();
        }

        [HttpDelete("{toDoId}")]
        public IActionResult DeleteToDoItem(Guid toDoId)
        {
            if (toDoId == null) return NotFound();

            var toDoItemFromRepo = _repository.GetItem(toDoId);
            if (toDoItemFromRepo == null) return NotFound();

            _repository.DeleteItem(toDoItemFromRepo);
            _repository.Save();

            return NoContent();
        }

    }
}
