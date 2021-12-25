using Devjobs.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Devjobs.Repositories
{
    public class CandidatesRepository : ICandidatesRepository
    {
        private DatabaseContext context;

        public CandidatesRepository(DatabaseContext context)
        {
            this.context = context;
        }
        public async Task<Candidate> AddCandidateAsync(Candidate candidate)
        {
            var user = await context.Users.FindAsync(candidate.UserId);
            if (user is null) return null;
            await context.Candidates.AddAsync(candidate);
            await SaveChangesAsync();
            return candidate;
        }

        public async Task DeleteCandidateAsync(int id)
        {
            Candidate candidate = await context.Candidates.FindAsync(id);
            if (candidate != null)
            {
                context.Candidates.Remove(candidate);
                await SaveChangesAsync();
            }

        }
        public async Task<Candidate> GetCandidateByIdAsync(int id)
        {
            return await context.Candidates.FindAsync(id);
        }

        public async Task<Candidate> GetCandidateByUserIddAsync(int userId)
        {            
            return await context.Candidates.FirstOrDefaultAsync(candidate => candidate.UserId == userId);
        }

        public async Task<IEnumerable<Candidate>> GetCandidatesAsync()
        {
            return await context.Candidates.ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }

        public async Task UpdateCandidateAsync(Candidate candidate)
        {
            context.Entry(candidate).State = EntityState.Modified;
            await SaveChangesAsync();
        }
    }
}
