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
    public class TaskTextDtoesController : ControllerBase
    {
        private readonly ScavEditorApiContext _context;

        public TaskTextDtoesController(ScavEditorApiContext context)
        {
            _context = context;
        }

        // GET: api/TaskTextDtoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskTextDto>>> GetTaskTextDto()
        {
            return await _context.TaskTextDto.ToListAsync();
        }

        // GET: api/TaskTextDtoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskTextDto>> GetTaskTextDto(int id)
        {
            var taskTextDto = await _context.TaskTextDto.FindAsync(id);

            if (taskTextDto == null)
            {
                return NotFound();
            }

            return taskTextDto;
        }

        // PUT: api/TaskTextDtoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTaskTextDto(int id, TaskTextDto taskTextDto)
        {
            if (id != taskTextDto.Id)
            {
                return BadRequest();
            }

            _context.Entry(taskTextDto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskTextDtoExists(id))
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

        // POST: api/TaskTextDtoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TaskTextDto>> PostTaskTextDto(TaskTextDto taskTextDto)
        {
            _context.TaskTextDto.Add(taskTextDto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTaskTextDto", new { id = taskTextDto.Id }, taskTextDto);
        }

        // DELETE: api/TaskTextDtoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaskTextDto(int id)
        {
            var taskTextDto = await _context.TaskTextDto.FindAsync(id);
            if (taskTextDto == null)
            {
                return NotFound();
            }

            _context.TaskTextDto.Remove(taskTextDto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TaskTextDtoExists(int id)
        {
            return _context.TaskTextDto.Any(e => e.Id == id);
        }
    }
}
