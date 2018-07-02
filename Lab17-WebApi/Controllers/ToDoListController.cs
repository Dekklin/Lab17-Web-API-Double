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
        /// <summary>
        /// Creates a private dummy of the DB for reference
        /// </summary>
        /// <param name="context"></param>
        public ToDoListController(ToDoContext context)
        {
            _context = context;
            if (_context.ToDoLists.Count() == 0)
            {
                _context.ToDos.AddAsync(new ToDo { Title = "Item1" });
                _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Grabs all the ToDoLists in the DB
        /// </summary>
        /// <returns>A list of JSON objects</returns>
        [HttpGet]
        public IEnumerable<ToDoList> GetToDoLists()
        {
            return _context.ToDoLists;
        }

        /// <summary>
        /// Grabs a specific ToDoList through the ID in the route
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The specified List, (if its there)</returns>
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

        /// <summary>
        /// Creates a list from the JSON body we send it
        /// </summary>
        /// <param name="list"></param>
        /// <returns>The Get form of this ID</returns>
        [HttpPost(Name = "Post")]
        public async Task<IActionResult> PostToDoList([FromBody] ToDoList list)
      {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await _context.ToDoLists.AddAsync(list);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetToDoList", new { id = list.ID }, list);

        }

        /// <summary>
        /// Updates the specific item in our DB through the ID in the route
        /// </summary>
        /// <param name="id"></param>
        /// <param name="list"></param>
        /// <returns>Nothing</returns>
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

        /// <summary>
        /// Deletes a specified item in our DB through our route
        /// </summary>
        /// <param name="id"></param>
        /// <returns>nothing</returns>
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
