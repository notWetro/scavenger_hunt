using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScavEditor.Api.Data;
using ScavEditor.Api.Models;

namespace ScavEditor.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskTextsController : ControllerBase
    {
        private readonly ScavEditorApiContext _context;

        public TaskTextsController(ScavEditorApiContext context)
        {
            _context = context;
        }

        // GET: api/TaskTexts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskText>>> GetTaskText()
        {
            return await _context.TaskText.ToListAsync();
        }

        // GET: api/TaskTexts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskText>> GetTaskText(int id)
        {
            var taskText = await _context.TaskText.FindAsync(id);

            if (taskText == null)
            {
                return NotFound();
            }

            return taskText;
        }

        // PUT: api/TaskTexts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTaskText(int id, TaskText taskText)
        {
            if (id != taskText.Id)
            {
                return BadRequest();
            }

            _context.Entry(taskText).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskTextExists(id))
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

        // POST: api/TaskTexts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TaskText>> PostTaskText(TaskText taskText)
        {
            _context.TaskText.Add(taskText);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTaskText", new { id = taskText.Id }, taskText);
        }

        // DELETE: api/TaskTexts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaskText(int id)
        {
            var taskText = await _context.TaskText.FindAsync(id);
            if (taskText == null)
            {
                return NotFound();
            }

            _context.TaskText.Remove(taskText);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TaskTextExists(int id)
        {
            return _context.TaskText.Any(e => e.Id == id);
        }
    }
}
