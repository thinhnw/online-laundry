using Devjobs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Devjobs.Repositories
{
    public interface ICandidatesRepository
    {
        Task<IEnumerable<Candidate>> GetCandidatesAsync();
        Task<Candidate> GetCandidateByIdAsync(int id);
        Task<Candidate> GetCandidateByUserIddAsync(int userId);
        Task<Candidate> AddCandidateAsync(Candidate candidate);
        Task DeleteCandidateAsync(int id);
        Task UpdateCandidateAsync(Candidate candidate);
        Task SaveChangesAsync();
    }
}
