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
    public class TaskDtoesController : ControllerBase
    {
        private readonly ScavEditorApiContext _context;

        public TaskDtoesController(ScavEditorApiContext context)
        {
            _context = context;
        }

        // GET: api/TaskDtoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskDto>>> GetTaskDto()
        {
            return await _context.TaskDto.ToListAsync();
        }

        // GET: api/TaskDtoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskDto>> GetTaskDto(int id)
        {
            var taskDto = await _context.TaskDto.FindAsync(id);

            if (taskDto == null)
            {
                return NotFound();
            }

            return taskDto;
        }

        // PUT: api/TaskDtoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTaskDto(int id, TaskDto taskDto)
        {
            if (id != taskDto.Id)
            {
                return BadRequest();
            }

            _context.Entry(taskDto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskDtoExists(id))
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

        // POST: api/TaskDtoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TaskDto>> PostTaskDto(TaskDto taskDto)
        {
            _context.TaskDto.Add(taskDto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTaskDto", new { id = taskDto.Id }, taskDto);
        }

        // DELETE: api/TaskDtoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaskDto(int id)
        {
            var taskDto = await _context.TaskDto.FindAsync(id);
            if (taskDto == null)
            {
                return NotFound();
            }

            _context.TaskDto.Remove(taskDto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TaskDtoExists(int id)
        {
            return _context.TaskDto.Any(e => e.Id == id);
        }
    }
}
