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
        public async Task<Job>AddJobAsync(Job job)
        {
            var corporate = await context.Corporates.FindAsync(job.CorporateId);
            if (corporate is null) return null;
            await context.Jobs.AddAsync(job);
            await SaveChangesAsync();
            return job;
        }

        public async Task DeleteJobAsync(int id)
        {
            Job job = await context.Jobs.FindAsync(id);
            if (job != null)
            {
                context.Jobs.Remove(job);
                await SaveChangesAsync();
            }
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
            context.Entry(job).State = EntityState.Modified;
            await SaveChangesAsync();
        }
    }
}
