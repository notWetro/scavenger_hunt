using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScavEditor.Api.DTOs;
using ScavEditor.Api.Data;

namespace ScavEditor.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskQuestionAnswerDtoesController : ControllerBase
    {
        private readonly ScavEditorApiContext _context;

        public TaskQuestionAnswerDtoesController(ScavEditorApiContext context)
        {
            _context = context;
        }

        // GET: api/TaskQuestionAnswerDtoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskQuestionAnswerDto>>> GetTaskQuestionAnswerDto()
        {
            return await _context.TaskQuestionAnswerDto.ToListAsync();
        }

        // GET: api/TaskQuestionAnswerDtoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskQuestionAnswerDto>> GetTaskQuestionAnswerDto(int id)
        {
            var taskQuestionAnswerDto = await _context.TaskQuestionAnswerDto.FindAsync(id);

            if (taskQuestionAnswerDto == null)
            {
                return NotFound();
            }

            return taskQuestionAnswerDto;
        }

        // PUT: api/TaskQuestionAnswerDtoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTaskQuestionAnswerDto(int id, TaskQuestionAnswerDto taskQuestionAnswerDto)
        {
            if (id != taskQuestionAnswerDto.Id)
            {
                return BadRequest();
            }

            _context.Entry(taskQuestionAnswerDto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskQuestionAnswerDtoExists(id))
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

        // POST: api/TaskQuestionAnswerDtoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TaskQuestionAnswerDto>> PostTaskQuestionAnswerDto(TaskQuestionAnswerDto taskQuestionAnswerDto)
        {
            _context.TaskQuestionAnswerDto.Add(taskQuestionAnswerDto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTaskQuestionAnswerDto", new { id = taskQuestionAnswerDto.Id }, taskQuestionAnswerDto);
        }

        // DELETE: api/TaskQuestionAnswerDtoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaskQuestionAnswerDto(int id)
        {
            var taskQuestionAnswerDto = await _context.TaskQuestionAnswerDto.FindAsync(id);
            if (taskQuestionAnswerDto == null)
            {
                return NotFound();
            }

            _context.TaskQuestionAnswerDto.Remove(taskQuestionAnswerDto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TaskQuestionAnswerDtoExists(int id)
        {
            return _context.TaskQuestionAnswerDto.Any(e => e.Id == id);
        }
    }
}
