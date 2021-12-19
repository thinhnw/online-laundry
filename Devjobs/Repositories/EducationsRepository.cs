using Devjobs.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Devjobs.Repositories
{
    public class EducationsRepository : IEducationsRepository
    {
        private DatabaseContext context;

        public EducationsRepository(DatabaseContext context)
        {
            this.context = context;
        }
        public async Task<Education> AddEducationAsync(Education education)
        {
            var candidate = await context.Candidates.FindAsync(education.CandidateId);
            if (candidate is null) return null;
            await context.Educations.AddAsync(education);
            await SaveChangesAsync();
            return education;
        }

        public async Task DeleteEducationAsync(int id)
        {
            Education education = await context.Educations.FindAsync(id);
            if (education != null)
            {
                context.Educations.Remove(education);
                await SaveChangesAsync();
            }
        }

        public async Task<Education> GetEducationByIdAsync(int id)
        {
            return await context.Educations.FindAsync(id);
        }

        public async Task<IEnumerable<Education>> GetEducationsAsync()
        {
            return await context.Educations.ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }

        public async Task UpdateEducationAsync(Education education)
        {
            context.Entry(education).State = EntityState.Modified;
            await SaveChangesAsync();
        }
    }
}
