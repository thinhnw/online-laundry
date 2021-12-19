using Devjobs.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Devjobs.Repositories
{
    public class WorkExperiencesRepository : IWorkExperiencesRepository
    {
        private DatabaseContext context;

        public WorkExperiencesRepository(DatabaseContext context)
        {
            this.context = context;
        }
        public async Task<WorkExperience> AddWorkExperienceAsync(WorkExperience workExperience)
        {
            var candidate = await context.Candidates.FindAsync(workExperience.CandidateId);
            if (candidate is null) return null;
            await context.WorkExperiences.AddAsync(workExperience);
            await SaveChangesAsync();
            return workExperience;
        }

        public async Task DeleteWorkExperienceAsync(int id)
        {
            WorkExperience workExperience = await context.WorkExperiences.FindAsync(id);
            if (workExperience != null)
            {
                context.WorkExperiences.Remove(workExperience);
                await SaveChangesAsync();
            }
        }

        public async Task<WorkExperience> GetWorkExperienceByIdAsync(int id)
        {
            return await context.WorkExperiences.FindAsync(id);
        }

        public async Task<IEnumerable<WorkExperience>> GetWorkExperiencesAsync()
        {
            return await context.WorkExperiences.ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }

        public async Task UpdateWorkExperienceAsync(WorkExperience workExperience)
        {
            context.Entry(workExperience).State = EntityState.Modified;
            await SaveChangesAsync();
        }
    }
}
