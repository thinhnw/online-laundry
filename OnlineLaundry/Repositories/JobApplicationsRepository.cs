using OnlineLaundry.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineLaundry.Repositories
{
    public class JobApplicationsRepository : IJobApplicationsRepository
    {
        private DatabaseContext context;

        public JobApplicationsRepository(DatabaseContext context)
        {
            this.context = context;
        }
        public async Task<JobApplication> AddJobApplicationAsync(JobApplication jobApplication)
        {
            Job job = await context.Jobs.FindAsync(jobApplication.JobId);
            Candidate candidate = await context.Candidates.FindAsync(jobApplication.CandidateId);
            if (job is null || candidate is null) return null;
            await context.JobApplications.AddAsync(jobApplication);
            await SaveChangesAsync();
            return jobApplication;
        }

        public async Task DeleteJobApplicationAsync(int id)
        {
            JobApplication jobApplication = await context.JobApplications.FindAsync(id);
            if (jobApplication != null)
            {
                context.JobApplications.Remove(jobApplication);
                await SaveChangesAsync();

            };
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
            context.Entry(jobApplication).State = EntityState.Modified;
            await SaveChangesAsync();
        }
    }
}
