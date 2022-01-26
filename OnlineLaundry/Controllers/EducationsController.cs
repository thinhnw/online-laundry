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
    public class EducationsController : ControllerBase
    {
        private readonly IEducationsRepository  repository;

        public EducationsController(IEducationsRepository repository)
        {
            this.repository = repository;
        }

        // GET: api/Educations
        [HttpGet]
        public async Task<IEnumerable<EducationDto>> GetEducations()
        {
            return (await repository.GetEducationsAsync()).Select(item => item.AsDto());
        }

        // GET: api/Educations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EducationDto>> GetEducation(int id)
        {
            var education = await repository.GetEducationByIdAsync(id);

            if (education == null)
            {
                return NotFound();
            }

            return education.AsDto();
        }

        // PUT: api/Educations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEducation(int id, EducationDto educationDto)
        {
            var educationInDb = await repository.GetEducationByIdAsync(id);
            if (educationInDb is null)
            {
                return NotFound();
            }
            Education education = educationInDb with
            {
                CandidateId=educationDto.CandidateId,
                City=educationDto.City,
                Country=educationDto.Country,
                Degree=educationDto.Degree,
                FieldOfStudy=educationDto.FieldOfStudy,
                FromTime=educationDto.FromTime,
                School=educationDto.School,
                ToTime=educationDto.ToTime
            };
            await repository.UpdateEducationAsync(education);
            return NoContent();
        }

        // POST: api/Educations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Education>> PostEducation(EducationDto educationDto)
        {
            Education education = new()
            {
                CandidateId = educationDto.CandidateId,
                City = educationDto.City,
                Country = educationDto.Country,
                Degree = educationDto.Degree,
                FieldOfStudy = educationDto.FieldOfStudy,
                FromTime = educationDto.FromTime,
                School = educationDto.School,
                ToTime = educationDto.ToTime
            };
            await repository.AddEducationAsync(education);

            return CreatedAtAction("GetCandidate", new { id = education.Id }, education);
        }

        // DELETE: api/Educations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEducation(int id)
        {
            var education = await repository.GetEducationByIdAsync(id);
            if (education == null)
            {
                return NotFound();
            }

            await repository.DeleteEducationAsync(id);
            return NoContent();
        }

    }
}
