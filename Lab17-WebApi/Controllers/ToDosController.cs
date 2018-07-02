using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab17_WebApi.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab17_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDosController : ControllerBase
    {
        private readonly ToDoContext _context;
        /// <summary>
        /// Creates a private reference to the database and creates a blank ToDo if none exist
        /// </summary>
        /// <param name="context"></param>
        public ToDosController(ToDoContext context)
        {
            _context = context;
            if(_context.ToDos.Count() == 0)
            {
                _context.ToDos.Add(new ToDo { Title = "Item1" });
                _context.SaveChanges();
            }
        }

        /// <summary>
        /// Retrieves all the ToDo items
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<List<ToDo>> GetAll()
        {
            return _context.ToDos.ToList();
        }

        // GET: api/ToDos/5
        [HttpGet("{id}", Name = "GetToDo")]
        public async Task<ActionResult<ToDo>> GetById(int id)
        {
            var item = await _context.ToDos.FindAsync(id);
            if(item == null)
            {
                return NotFound();
            }
            return item;
        }


        // POST: api/ToDos
        [HttpPost]
        public async Task<IActionResult> Create(ToDo item)
        {
            await _context.ToDos.AddAsync(item);
            await _context.SaveChangesAsync();
            return CreatedAtRoute("GetTodo", new { id = item.ID }, item);
        }

        // PUT: api/ToDos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute]int id,[FromBody] ToDo item)
        {
            item.ID = id;
            if (id != item.ID)
                return BadRequest();
            var todo = _context.ToDos.Find(id);
            if(todo == null)
            {
                return RedirectToAction("Post", item);
            }
            todo.Finished = item.Finished;
            todo.Title = item.Title;
            _context.ToDos.Update(todo);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var todo = _context.ToDos.Find(id);
            if(todo == null)
            {
                return NotFound();
            }
            _context.ToDos.Remove(todo);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
