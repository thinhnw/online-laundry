using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Devjobs.Models;
using Devjobs.Repositories;
using Devjobs.Dtos;

namespace Devjobs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidatesController : ControllerBase
    {
        private readonly DatabaseContext context;
        private readonly ICandidatesRepository repository;
        public CandidatesController(ICandidatesRepository repository, DatabaseContext context)
        {
            this.context = context;
            this.repository = repository;
        }

        // GET: api/Candidates
        [HttpGet]
        public async Task<IEnumerable<Candidate>> GetCandidates()
        {
            return await repository.GetCandidatesAsync();
        }

        // GET: api/Candidates/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Candidate>> GetCandidate(int id)
        {
            var candidate = await repository.GetCandidateByIdAsync(id);

            if (candidate == null)
            {
                return NotFound();
            }

            return candidate;
        }

        // PUT: api/Candidates/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCandidate(int id, Candidate candidate)
        {
            if (id != candidate.Id)
            {
                return BadRequest();
            }

            context.Entry(candidate).State = EntityState.Modified;

            try
            {
                await repository.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CandidateExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Candidates
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Candidate>> PostCandidate(CandidateDto candidateDto)
        {
            Candidate candidate = new()
            {
                Education = candidateDto.Education,
                YearsOfExperience = candidateDto.YearsOfExperience,
                CV = candidateDto.CV,
                UserId = candidateDto.UserId
            };
            await repository.AddCandidateAsync(candidate);
            await repository.SaveChangesAsync();

            return CreatedAtAction("GetCandidate", new { id = candidate.Id }, candidate);
        }

        // DELETE: api/Candidates/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCandidate(int id)
        {
            var candidate = await context.Candidates.FindAsync(id);
            if (candidate == null)
            {
                return NotFound();
            }

            await repository.DeleteCandidateAsync(id);
            await context.SaveChangesAsync();
            return NoContent();
        }

        private bool CandidateExists(int id)
        {
            return context.Candidates.Any(e => e.Id == id);
        }
    }
}
