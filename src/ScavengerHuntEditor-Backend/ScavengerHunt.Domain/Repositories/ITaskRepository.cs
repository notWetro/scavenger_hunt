using ScavengerHunt.Domain.Entities;

namespace ScavengerHunt.Domain.Repositories
{
    public interface ITaskRepository
    {
        public Task<TaskBase?> GetByIdAsync(int id);
        public Task<IEnumerable<TaskBase>> GetAll();

        public Task<int> AddAsync(TaskBase task);

        public Task<int[]> AddRangeAsync(IEnumerable<TaskBase> tasks);

        public Task<bool> UpdateAsync(TaskBase task);
        public Task<TaskBase?> DeleteByIdAsync(int id);
    }
}
