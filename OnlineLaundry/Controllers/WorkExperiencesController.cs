using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineLaundry.Models;
using OnlineLaundry.Repositories;
using OnlineLaundry.Dtos;

namespace OnlineLaundry.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkExperiencesController : ControllerBase
    {
        private readonly IWorkExperiencesRepository repository;
        public WorkExperiencesController(IWorkExperiencesRepository repository)
        {
            this.repository = repository;
        }

        // GET: api/WorkExperiences
        [HttpGet]
        public async Task<IEnumerable<WorkExperienceDto>> GetWorkExperiences()
        {
            return (await repository.GetWorkExperiencesAsync()).Select(item => item.AsDto());
        }

        // GET: api/WorkExperiences/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WorkExperienceDto>> GetWorkExperience(int id)
        {
            var workExperience = await repository.GetWorkExperienceByIdAsync(id);

            if (workExperience == null)
            {
                return NotFound();
            }

            return workExperience.AsDto();
        }

        // PUT: api/WorkExperiences/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorkExperience(int id, WorkExperienceDto workExperienceDto)
        {
            var workExperienceInDb = await repository.GetWorkExperienceByIdAsync(id);
            if (workExperienceInDb is null)
            {
                return NotFound();
            }
            WorkExperience workExperience = workExperienceInDb with
            {
                CandidateId=workExperienceDto.CandidateId,
                City=workExperienceDto.City,
                Country=workExperienceDto.Country,
                Description=workExperienceDto.Description,
                FromTime=workExperienceDto.FromTime,
                JobTitle=workExperienceDto.JobTitle,
                Organization=workExperienceDto.Organization,
                ToTime=workExperienceDto.ToTime

            };
            await repository.UpdateWorkExperienceAsync(workExperience);
            return NoContent();
        }

        // POST: api/WorkExperiences
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<WorkExperience>> PostWorkExperience(WorkExperienceDto workExperienceDto)
        {
            WorkExperience workExperience = new()
            {
                CandidateId = workExperienceDto.CandidateId,
                City = workExperienceDto.City,
                Country = workExperienceDto.Country,
                Description = workExperienceDto.Description,
                FromTime = workExperienceDto.FromTime,
                JobTitle = workExperienceDto.JobTitle,
                Organization = workExperienceDto.Organization,
                ToTime = workExperienceDto.ToTime
            };
            await repository.AddWorkExperienceAsync(workExperience);

            return CreatedAtAction("GetCandidate", new { id = workExperience.Id }, workExperience);
        }

        // DELETE: api/WorkExperiences/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkExperience(int id)
        {
            var workExperience = await repository.GetWorkExperienceByIdAsync(id);
            if (workExperience == null)
            {
                return NotFound();
            }

            await repository.DeleteWorkExperienceAsync(id);
            return NoContent();
        }

    }
}
