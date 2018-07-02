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

        public ToDosController(ToDoContext context)
        {
            _context = context;
            if(_context.ToDos.Count() == 0)
            {
                _context.ToDos.AddAsync(new ToDo { Title = "Item1" });
                _context.SaveChangesAsync();
            }
        }

        // GET: api/ToDos
        [HttpGet]
        public ActionResult<List<ToDo>> GetAll()
        {
            return _context.ToDos.ToList();
        }

        // GET: api/ToDos/5
        [HttpGet("{id}:int")]
        public ActionResult<ToDo> GetById(long id)
        {
            var item = _context.ToDos.Find(id);
            if(item == null)
            {
                return NotFound();
            }
            return item;
        }


        // POST: api/ToDos
        [HttpPost]
        public IActionResult Create([FromBody]ToDo item)
        {
            _context.ToDos.AddAsync(item);
            _context.SaveChangesAsync();
            return CreatedAtRoute("GetTodo", new { id = item.ID }, item);
        }

        // PUT: api/ToDos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id,[FromBody] ToDo item)
        {
            if (id != item.ID)
                return BadRequest();
            var todo = _context.ToDos.Find(id);
            if(todo == null)
            {
                return RedirectToAction("Post", item);
            }
            todo.Finished = item.Finished;
            todo.Title = item.Title;
            await _context.ToDos.AddAsync(todo);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
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
