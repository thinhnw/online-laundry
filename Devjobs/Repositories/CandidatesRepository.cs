﻿using Devjobs.Models;
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
        public async Task AddCandidateAsync(Candidate candidate)
        {
            await context.Candidates.AddAsync(candidate);
        }

        public async Task DeleteCandidateAsync(int id)
        {
            Candidate candidate = await context.Candidates.FindAsync(id);
            context.Candidates.Remove(candidate);
        }
        public async Task<Candidate> GetCandidateByIdAsync(int id)
        {
            return await context.Candidates.FindAsync(id);
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
            var candidateInDb = await context.Candidates.FindAsync(candidate.Id);
            candidateInDb = candidate;
            await SaveChangesAsync();
        }
    }
}