using Microsoft.AspNetCore.Mvc;
using ScavengerHunt.Domain.Entities;
using ScavengerHunt.Domain.Repositories;

namespace ScavengerHunt.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController(ITaskRepository repository) : ControllerBase
    {
        private readonly ITaskRepository _taskRepository = repository;

        // GET: api/Tasks/5
        [HttpGet("/{id}")]
        public async Task<ActionResult<TaskText>> GetTask(int id)
        {
            var taskt = await _taskRepository.GetTextByIdAsync(id);
            if (taskt is not null)
                return Ok(taskt);

            return NotFound();
        }

        // PUT: api/Tasks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("/{id}")]
        public async Task<IActionResult> PutTask(int id, TaskText task)
        {
            if (id != task.Id)
                return BadRequest();

            var taskt = await _taskRepository.GetTextByIdAsync(id);
            if (taskt is not null)
            {
                if (await _taskRepository.UpdateAsync(task))
                    return Ok();
            }
            return NotFound();
        }

        // POST: api/Tasks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TaskText>> PostTaskText(TaskText task)
        {
            var id = await _taskRepository.AddAsync(task);

            if (id <= 0)
                return BadRequest();

            return CreatedAtAction(nameof(PostTaskText), new { id = task.Id });
        }

        // DELETE: api/Tasks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var task = await _taskRepository.DeleteByIdAsync(id);

            if (task is null)
                return NotFound();

            return Ok(task);
        }
    }
}
