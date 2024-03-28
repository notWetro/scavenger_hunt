using ScavengerHunt.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScavengerHunt.Domain.Repositories
{
    public interface ITaskRepository
    {
        public Task<TaskBase> GetByIdAsync(int id);
        public Task Add(TaskBase taskBase);
    }
}
