using Devjobs.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Devjobs.Repositories
{
    public class SkillsRepository : ISkillsRepository
    {
        private DatabaseContext context;

        public SkillsRepository(DatabaseContext context)
        {
            this.context = context;
        }
        public async Task<Skill> AddSkillAsync(Skill skill)
        {
            var candidate = await context.Candidates.FindAsync(skill.CandidateId);
            if (candidate is null) return null;
            await context.Skills.AddAsync(skill);
            await SaveChangesAsync();
            return skill;
        }

        public async Task DeleteSkillAsync(int id)
        {
            Skill skill = await context.Skills.FindAsync(id);
            if (skill != null)
            {
                context.Skills.Remove(skill);
                await SaveChangesAsync();
            }
        }

        public async Task<Skill> GetSkillByIdAsync(int id)
        {
            return await context.Skills.FindAsync(id);
        }

        public async Task<IEnumerable<Skill>> GetSkillsAsync()
        {
            return await context.Skills.ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }

        public async Task UpdateSkillAsync(Skill skill)
        {
            context.Entry(skill).State = EntityState.Modified;
            await SaveChangesAsync();
        }
    }
}
