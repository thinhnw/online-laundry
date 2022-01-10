using Devjobs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Devjobs.Repositories
{
    public interface ISkillsRepository
    {
        Task<IEnumerable<Skill>> GetSkillsAsync();
        Task<Skill> GetSkillByIdAsync(int id);
        Task<Skill> AddSkillAsync(Skill skill);
        Task DeleteSkillAsync(int id);
        Task UpdateSkillAsync(Skill skill);
        Task SaveChangesAsync();
    }
}
