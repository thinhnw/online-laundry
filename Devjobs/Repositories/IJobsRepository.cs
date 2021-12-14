using Devjobs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Devjobs.Repositories
{
    public interface IJobsRepository
    {
        Task<IEnumerable<Job>> GetJobsAsync();
        Task<Job> GetJobByIdAsync(int id);
        Task<Job> AddJobAsync(Job job);
        Task DeleteJobAsync(int id);
        Task UpdateJobAsync(Job job);
        Task SaveChangesAsync();
    }
}
