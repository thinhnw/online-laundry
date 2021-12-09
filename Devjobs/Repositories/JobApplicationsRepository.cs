using Devjobs.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Devjobs.Repositories
{
    public class JobApplicationsRepository : IJobApplicationsRepository
    {
        private DatabaseContext context;

        public JobApplicationsRepository(DatabaseContext context)
        {
            this.context = context;
        }
        public async Task AddJobApplicationAsync(JobApplication jobApplication)
        {
            await context.JobApplications.AddAsync(jobApplication);
        }

        public async Task DeleteJobApplicationAsync(int id)
        {
            JobApplication jobApplication = await context.JobApplications.FindAsync(id);
            context.JobApplications.Remove(jobApplication);
        }

        public async Task<JobApplication> GetJobApplicationByIdAsync(int id)
        {
            return await context.JobApplications.FindAsync(id);
        }

        public async Task<IEnumerable<JobApplication>> GetJobApplicationsAsync()
        {
            return await context.JobApplications.ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }

        public async Task UpdateJobApplicationAsync(JobApplication jobApplication)
        {
            var jobInDb = await context.JobApplications.FindAsync(jobApplication.Id);
            jobInDb = jobApplication;
            await SaveChangesAsync();
        }
    }
}
