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
using Microsoft.AspNetCore.Authorization;

namespace Devjobs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidatesController : BaseController
    {
        private readonly ICandidatesRepository candidates;
        private readonly IEducationsRepository educations;
        public CandidatesController(ICandidatesRepository candidates, IEducationsRepository educations, IUsersRepository users) : base(users)
        {
            this.candidates = candidates;
            this.educations = educations;
        }

        // GET: api/Candidates
        [HttpGet]
        public async Task<IEnumerable<CandidateDto>> GetCandidates()
        {
            return (await candidates.GetCandidatesAsync()).Select(item=>item.AsDto());
        }

        // GET: api/Candidates/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CandidateDto>> GetCandidate(int id)
        {
            var candidate = await candidates.GetCandidateByIdAsync(id);

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
            var candidateInDb = await candidates.GetCandidateByIdAsync(id);
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
            await candidates.UpdateCandidateAsync(candidate);
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
            await candidates.AddCandidateAsync(candidate);

            return CreatedAtAction("GetCandidate", new { id = candidate.Id }, candidate);
        }

        [HttpPost("personal-details")]
        [Authorize]
        public async Task<ActionResult<CandidateDto>> UpdatePersonalProfile(CandidatePersonalDetailsDto dto)
        {

            User user = await GetCurrentUser();
            Candidate candidate = user.Candidate;

            candidate.FirstName = dto.FirstName;
            candidate.LastName = dto.LastName;
            candidate.Country = dto.Country;
            candidate.City = dto.City;
            candidate.Address = dto.Address;
            candidate.Phone = dto.Phone;

            await candidates.SaveChangesAsync();
            return NoContent();
        }


        [HttpPost("educations")]
        [Authorize]
        public async Task<ActionResult<CandidateDto>> UpdateEducations(List<UpdateEducationDto> updateEduList)
        {

            User user = await GetCurrentUser();
            Candidate candidate = user.Candidate;
            

            await candidates.SaveChangesAsync();
            return candidate.AsDto();
        }

        // DELETE: api/Candidates/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCandidate(int id)
        {
            var candidate = await candidates.GetCandidateByIdAsync(id);
            if (candidate == null)
            {
                return NotFound();
            }

            await candidates.DeleteCandidateAsync(id);
            return NoContent();
        }

    }
}
