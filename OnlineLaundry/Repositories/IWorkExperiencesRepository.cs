using OnlineLaundry.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineLaundry.Repositories
{
    public interface IWorkExperiencesRepository
    {
        Task<IEnumerable<WorkExperience>> GetWorkExperiencesAsync();
        Task<WorkExperience> GetWorkExperienceByIdAsync(int id);
        Task<WorkExperience> AddWorkExperienceAsync(WorkExperience workExperience);
        Task DeleteWorkExperienceAsync(int id);
        Task UpdateWorkExperienceAsync(WorkExperience workExperience);
        Task SaveChangesAsync();
    }
}
