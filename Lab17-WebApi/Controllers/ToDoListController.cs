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
    public class ToDoListController : ControllerBase
    {

        private readonly ToDoContext _context;

        public ToDoListController(ToDoContext context)
        {
            _context = context;
            if (_context.ToDoLists.Count() == 0)
            {
                _context.ToDos.AddAsync(new ToDo { Title = "Item1" });
                _context.SaveChangesAsync();
            }
        }

        //GET: api/ToDoList
        [HttpGet]
        public IEnumerable<ToDoList> GetToDoLists()
        {
            return _context.ToDoLists;
        }

        // GET: api/ToDoList/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> GetToDoList([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            ToDoList list = await _context.ToDoLists.FindAsync(id);
            var todo = _context.ToDos.Where(x => x.ListId == id).ToList();
            list.Contents = todo;
            if (list == null)
                return NotFound();
            return Ok(list);
        }

        // POST: api/ToDoList
        [HttpPost(Name = "Post")]
        public async Task<IActionResult> PostToDoList([FromBody] ToDoList list)
      {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await _context.ToDoLists.AddAsync(list);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetToDoList", new { id = list.ID }, list);

        }

        // PUT: api/ToDoList/5
        [HttpPut("{id}", Name = "Put")]
        public async Task<IActionResult> PutToDoList([FromRoute]int id, [FromBody] ToDoList list)
        {
            list.ID = id;
            //ToDoList y = await _context.ToDoLists.FirstOrDefaultAsync(x => x.ID == id);
            //y = list;

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (id != list.ID)
                return BadRequest();
            _context.Entry(list).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ToDoListExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}", Name = "Delete")]
        public async Task<IActionResult> DeleteToDoList([FromRoute]int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var list = await _context.ToDoLists.FindAsync(id);
            if (list == null)
                return NotFound();
            _context.ToDoLists.Remove(list);
            await _context.SaveChangesAsync();
            return Ok(list);
        }
        /// <summary>
        /// Checks if there are any To Do Lists with that id (used in Put call)
        /// </summary>
        /// <param name="id"></param>
        /// <returns>True or False</returns>
        private bool ToDoListExists(int id)
        {
            return _context.ToDoLists.Any(k => k.ID == id);
        }
    }
}
