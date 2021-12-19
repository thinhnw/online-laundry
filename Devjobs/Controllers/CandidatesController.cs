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
        private readonly ICandidatesRepository repository;
        public CandidatesController(ICandidatesRepository repository)
        {
            this.repository = repository;
        }

        // GET: api/Candidates
        [HttpGet]
        public async Task<IEnumerable<CandidateDto>> GetCandidates()
        {
            return (await repository.GetCandidatesAsync()).Select(item=>item.AsDto());
        }

        // GET: api/Candidates/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CandidateDto>> GetCandidate(int id)
        {
            var candidate = await repository.GetCandidateByIdAsync(id);

            if (candidate == null)
            {
                return NotFound();
            }

            return candidate.AsDto();
        }

        // PUT: api/Candidates/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCandidate(int id, CandidateDto candidateDto)
        {
            var candidateInDb = await repository.GetCandidateByIdAsync(id);
            if (candidateInDb is null)
            {
                return NotFound();
            }
            Candidate candidate = candidateInDb with
            {
                UserId = candidateDto.UserId,
                City = candidateDto.City,
                Country = candidateDto.Country,
                Address = candidateDto.Address,
                FirstName = candidateDto.FirstName,
                LastName = candidateDto.LastName,
                Phone = candidateDto.Phone,
            };
            await repository.UpdateCandidateAsync(candidate);
            return NoContent();
        }

        // POST: api/Candidates
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Candidate>> PostCandidate(CandidateDto candidateDto)
        {
            Candidate candidate = new()
            {
                UserId = candidateDto.UserId,
                City = candidateDto.City,
                Country = candidateDto.Country,
                Address = candidateDto.Address,
                FirstName = candidateDto.FirstName,
                LastName = candidateDto.LastName,
                Phone = candidateDto.Phone,
            };
            await repository.AddCandidateAsync(candidate);

            return CreatedAtAction("GetCandidate", new { id = candidate.Id }, candidate);
        }

        // DELETE: api/Candidates/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCandidate(int id)
        {
            var candidate = await repository.GetCandidateByIdAsync(id);
            if (candidate == null)
            {
                return NotFound();
            }

            await repository.DeleteCandidateAsync(id);
            return NoContent();
        }

    }
}
