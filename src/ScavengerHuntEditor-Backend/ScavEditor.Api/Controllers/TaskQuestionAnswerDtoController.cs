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
    public class TaskQuestionAnswerDtoController : ControllerBase
    {
        private readonly ScavEditorApiContext _context;
        private readonly IMapper _mapper;

        public TaskQuestionAnswerDtoController(ScavEditorApiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/TaskQuestionAnswerDtoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskQuestionAnswerDto>>> GetTaskQuestionAnswerDto()
        {
            var tasks = await _context.TaskQuestionAnswer.ToListAsync();
            var taskDtos = _mapper.Map<List<TaskQuestionAnswerDto>>(tasks);
            return taskDtos;
        }

        // GET: api/TaskQuestionAnswerDtoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskQuestionAnswerDto>> GetTaskQuestionAnswerDto(int id)
        {
            var task = await _context.TaskQuestionAnswer.FindAsync(id);
            var taskDto = _mapper.Map<TaskQuestionAnswerDto>(task);

            if (taskDto is null)
                return NotFound();

            return taskDto;
        }

        // PUT: api/TaskQuestionAnswerDtoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTaskQuestionAnswerDto(int id, TaskQuestionAnswerDto taskQuestionAnswerDto)
        {
            if (id != taskQuestionAnswerDto.Id)
                return BadRequest();

            var task = await _context.TaskQuestionAnswer.FindAsync(id);

            if (task is null)
                return NotFound();

            _context.Entry(task).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskQuestionAnswerExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // POST: api/TaskQuestionAnswerDtoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TaskQuestionAnswerDto>> PostTaskQuestionAnswerDto(TaskQuestionAnswerDto taskQuestionAnswerDto)
        {
            _context.TaskQuestionAnswer.Add(_mapper.Map<TaskQuestionAnswer>(taskQuestionAnswerDto));
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTaskQuestionAnswerDto", new { id = taskQuestionAnswerDto.Id }, taskQuestionAnswerDto);
        }

        // DELETE: api/TaskQuestionAnswerDtoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaskQuestionAnswerDto(int id)
        {
            var taskQuestionAnswerDto = await _context.TaskQuestionAnswer.FindAsync(id);
            if (taskQuestionAnswerDto is null)
                return NotFound();

            _context.TaskQuestionAnswer.Remove(taskQuestionAnswerDto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TaskQuestionAnswerExists(int id)
        {
            return _context.TaskQuestionAnswer.Any(e => e.Id == id);
        }
    }
}
