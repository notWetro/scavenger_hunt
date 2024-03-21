using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScavEditor.Api.DTOs;
using ScavEditor.Api.Data;
using AutoMapper;
using ScavEditor.Api.Models;

namespace ScavEditor.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskTextDtoController : ControllerBase
    {
        private readonly ScavEditorApiContext _context;
        private readonly IMapper _mapper;


        public TaskTextDtoController(ScavEditorApiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/TaskTextDtoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskTextDto>>> GetTaskTextDto()
        {
            var tasks = await _context.TaskText.ToListAsync();
            var taskDtos = _mapper.Map<List<TaskTextDto>>(tasks);
            return taskDtos;
        }

        // GET: api/TaskTextDtoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskTextDto>> GetTaskTextDto(int id)
        {
            var task = await _context.TaskText.FindAsync(id);
            var taskDto = _mapper.Map<TaskTextDto>(task);

            if (taskDto is null)
                return NotFound();

            return taskDto;
        }

        // PUT: api/TaskTextDtoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTaskTextDto(int id, TaskTextDto taskTextDto)
        {
            if (id != taskTextDto.Id)
                return BadRequest();

            var task = await _context.TaskText.FindAsync(id);

            if (task is null)
                return NotFound();

            _context.Entry(task).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskTextExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // POST: api/TaskTextDtoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TaskTextDto>> PostTaskTextDto(TaskTextDto taskTextDto)
        {
            _context.TaskText.Add(_mapper.Map<TaskText>(taskTextDto));
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTaskTextDto", new { id = taskTextDto.Id }, taskTextDto);
        }

        // DELETE: api/TaskTextDtoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaskTextDto(int id)
        {
            var taskTextDto = await _context.TaskText.FindAsync(id);
            if (taskTextDto is null)
                return NotFound();

            _context.TaskText.Remove(taskTextDto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TaskTextExists(int id)
        {
            return _context.TaskText.Any(e => e.Id == id);
        }
    }
}
