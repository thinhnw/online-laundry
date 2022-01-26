using OnlineLaundry.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineLaundry.Repositories
{
    public interface IJobApplicationsRepository
    {
        Task<IEnumerable<JobApplication>> GetJobApplicationsAsync();
        Task<JobApplication> GetJobApplicationByIdAsync(int id);
        Task<JobApplication> AddJobApplicationAsync(JobApplication jobApplication);
        Task DeleteJobApplicationAsync(int id);
        Task UpdateJobApplicationAsync(JobApplication jobApplication);
        Task SaveChangesAsync();
    }
}
