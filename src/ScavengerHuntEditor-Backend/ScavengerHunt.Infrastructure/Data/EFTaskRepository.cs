using Microsoft.EntityFrameworkCore;
using ScavengerHunt.Domain.Entities;
using ScavengerHunt.Infrastructure;

namespace ScavengerHunt.Domain.Repositories
{
    public sealed class EFTaskRepository(ScavHuntDbContext dbContext) : ITaskRepository
    {
        private readonly ScavHuntDbContext _context = dbContext;

        public Task<TaskQuestionAnswer?> GetQuestionAnswerByIdAsync(int id)
        {
            throw new NotImplementedException();
            //try
            //{
            //    var task = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id);

            //    if(task is TaskQuestionAnswer taskQA)
            //        return taskQA;

            //    return null;
            //}
            //catch (Exception ex)
            //{
            //    _ = ex.Message;
            //    return null;
            //}
        }

        public async Task<TaskText?> GetTextByIdAsync(int id)
        {
            try
            {
                var task = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id);

                if (task is TaskText taskText)
                    return taskText;

                return null;
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                return null;
            }
        }

        public Task<int> AddAsync(TaskQuestionAnswer task)
        {
            throw new NotImplementedException();
            //try
            //{
            //    await _context.Tasks.AddAsync(task);
            //    _context.SaveChanges();
            //    return task.Id;
            //}
            //catch (Exception ex)
            //{
            //    _ = ex.Message;
            //    return -1;
            //}
        }

        public async Task<int> AddAsync(TaskText task)
        {
            try
            {
                await _context.Tasks.AddAsync(task);
                _context.SaveChanges();
                return task.Id;
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                return -1;
            }
        }

        public async Task<bool> UpdateAsync(TaskQuestionAnswer task)
        {
            try
            {
                _context.Entry(task).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                return false;
            }
        }

        public async Task<bool> UpdateAsync(TaskText task)
        {
            try
            {
                _context.Entry(task).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                return false;
            }
        }

        public async Task<TaskBase?> DeleteByIdAsync(int id)
        {
            try
            {
                var scavengerTaskBase = await _context.Tasks.FindAsync(id);

                if (scavengerTaskBase is not null)
                {
                    _context.Tasks.Remove(scavengerTaskBase);
                    await _context.SaveChangesAsync();
                    return scavengerTaskBase;
                }
                return null;
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                return null;
            }
        }
    }
}
