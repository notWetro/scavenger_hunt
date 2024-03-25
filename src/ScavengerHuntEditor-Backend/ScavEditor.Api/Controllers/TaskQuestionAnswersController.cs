using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScavEditor.Api.Data;
using ScavEditor.Api.Models;

namespace ScavEditor.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskQuestionAnswersController : ControllerBase
    {
        private readonly ScavEditorApiContext _context;

        public TaskQuestionAnswersController(ScavEditorApiContext context)
        {
            _context = context;
        }

        // GET: api/TaskQuestionAnswers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskQuestionAnswer>>> GetTaskQuestionAnswer()
        {
            return await _context.TaskQuestionAnswer.ToListAsync();
        }

        // GET: api/TaskQuestionAnswers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskQuestionAnswer>> GetTaskQuestionAnswer(int id)
        {
            var taskQuestionAnswer = await _context.TaskQuestionAnswer.FindAsync(id);

            if (taskQuestionAnswer == null)
            {
                return NotFound();
            }

            return taskQuestionAnswer;
        }

        // PUT: api/TaskQuestionAnswers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTaskQuestionAnswer(int id, TaskQuestionAnswer taskQuestionAnswer)
        {
            if (id != taskQuestionAnswer.Id)
            {
                return BadRequest();
            }

            _context.Entry(taskQuestionAnswer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskQuestionAnswerExists(id))
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

        // POST: api/TaskQuestionAnswers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TaskQuestionAnswer>> PostTaskQuestionAnswer(TaskQuestionAnswer taskQuestionAnswer)
        {
            _context.TaskQuestionAnswer.Add(taskQuestionAnswer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTaskQuestionAnswer", new { id = taskQuestionAnswer.Id }, taskQuestionAnswer);
        }

        // DELETE: api/TaskQuestionAnswers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaskQuestionAnswer(int id)
        {
            var taskQuestionAnswer = await _context.TaskQuestionAnswer.FindAsync(id);
            if (taskQuestionAnswer == null)
            {
                return NotFound();
            }

            _context.TaskQuestionAnswer.Remove(taskQuestionAnswer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TaskQuestionAnswerExists(int id)
        {
            return _context.TaskQuestionAnswer.Any(e => e.Id == id);
        }
    }
}
