using Devjobs.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Devjobs.Repositories
{
    public class JobsRepository : IJobsRepository
    {
        private DatabaseContext context;

        public JobsRepository(DatabaseContext context)
        {
            this.context = context;
        }
        public async Task AddJobAsync(Job job)
        {
            await context.Jobs.AddAsync(job);
        }

        public async Task DeleteJobAsync(int id)
        {
            Job job = await context.Jobs.FindAsync(id);
            context.Jobs.Remove(job);
        }

        public async Task<Job> GetJobByIdAsync(int id)
        {
            return await context.Jobs.FindAsync(id);
        }

        public async Task<IEnumerable<Job>> GetJobsAsync()
        {
            return await context.Jobs.ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }

        public async Task UpdateJobAsync(Job job)
        {
            var jobInDb = await context.Jobs.FindAsync(job.Id);
            jobInDb = job;
            await SaveChangesAsync();
        }
    }
}
