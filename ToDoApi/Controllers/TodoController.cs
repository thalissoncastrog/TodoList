using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ToDoApi.Models;

namespace ToDoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly List<TodoItem> _todoItems = new List<TodoItem>();

        [HttpGet]
        public ActionResult<IEnumerable<TodoItem>> Get()
        {
            return _todoItems;
        }

        [HttpGet("{id}")]
        public ActionResult<TodoItem> Get(long id)
        {
            var todoItem = _todoItems.Find(item => item.Id == id);
            if (todoItem == null)
            {
                return NotFound();
            }
            return todoItem;
        }

        [HttpPost]
        public ActionResult<TodoItem> Post(TodoItem todoItem)
        {
            todoItem.Id = _todoItems.Count + 1;
            _todoItems.Add(todoItem);
            return CreatedAtAction(nameof(Get), new { id = todoItem.Id }, todoItem);
        }

        [HttpPut("{id}")]
        public IActionResult Put(long id, TodoItem todoItem)
        {
            var existingItem = _todoItems.Find(item => item.Id == id);
            if (existingItem == null)
            {
                return NotFound();
            }
            existingItem.Name = todoItem.Name;
            existingItem.IsComplete = todoItem.IsComplete;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var todoItem = _todoItems.Find(item => item.Id == id);
            if (todoItem == null)
            {
                return NotFound();
            }
            _todoItems.Remove(todoItem);
            return NoContent();
        }
    }
}
