using ScavengerHunt.Domain.Entities;

namespace ScavengerHunt.Domain.Repositories
{
    public interface ITaskRepository
    {

        public Task<TaskQuestionAnswer?> GetQuestionAnswerByIdAsync(int id);
        public Task<TaskText?> GetTextByIdAsync(int id);

        public Task<int> AddAsync(TaskQuestionAnswer task);
        public Task<int> AddAsync(TaskText task);

        public Task<bool> UpdateAsync(TaskQuestionAnswer task);
        public Task<bool> UpdateAsync(TaskText task);

        public Task<TaskBase?> DeleteByIdAsync(int id);
    }
}
