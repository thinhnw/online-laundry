using Devjobs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Devjobs.Repositories
{
    public interface IJobApplicationsRepository
    {
        Task<IEnumerable<JobApplication>> GetJobApplicationsAsync();
        Task<JobApplication> GetJobApplicationByIdAsync(int id);
        Task AddJobApplicationAsync(JobApplication jobApplication);
        Task DeleteJobApplicationAsync(int id);
        Task UpdateJobApplicationAsync(JobApplication jobApplication);
        Task SaveChangesAsync();
    }
}
