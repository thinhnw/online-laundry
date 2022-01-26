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
using Microsoft.AspNetCore.Authorization;

namespace OnlineLaundry.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidatesController : BaseController
    {
        private readonly ICandidatesRepository candidates;
        private readonly IEducationsRepository educations;
        private readonly IWorkExperiencesRepository works;
        private readonly ISkillsRepository skills;
        public CandidatesController(ICandidatesRepository candidates, IEducationsRepository educations, IWorkExperiencesRepository works, ISkillsRepository skills, IUsersRepository users) : base(users)
        {
            this.candidates = candidates;
            this.educations = educations;
            this.works = works;
            this.skills = skills;
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

            foreach (var edu in updateEduList)            
            {                
                
                if (edu.Id > 0)
                {
                    Education education = await educations.GetEducationByIdAsync((int) edu.Id);
                    if (education.CandidateId != candidate.Id)
                    {
                        continue;
                    }
                    education.Degree = edu.Degree;
                    education.FieldOfStudy = edu.FieldOfStudy;
                    education.School = edu.School;
                    education.City = edu.City;
                    education.Country = edu.Country;
                    education.FromTime = edu.FromTime;
                    education.ToTime = edu.ToTime;
                    education.CandidateId = candidate.Id;                    
                    await educations.UpdateEducationAsync(education);
                }
                else
                {
                    Education education = new Education
                    {
                        Degree = edu.Degree,
                        FieldOfStudy = edu.FieldOfStudy,
                        School = edu.School,
                        City = edu.City,
                        Country = edu.Country,
                        FromTime = edu.FromTime,
                        ToTime = edu.ToTime,
                        CandidateId = candidate.Id
                    };
                    await educations.AddEducationAsync(education);
                }
            }
            

            await candidates.SaveChangesAsync();
            return candidate.AsDto();
        }
        [HttpPost("work-experiences")]
        [Authorize]
        public async Task<ActionResult<CandidateDto>> UpdateWorkExperiences(List<UpdateWorkExperienceDto> updateWorkExperienceList)
        {

            User user = await GetCurrentUser();
            Candidate candidate = user.Candidate;

            foreach (var we in updateWorkExperienceList)
            {                
                if (we.Id > 0)
                {
                    WorkExperience work = await works.GetWorkExperienceByIdAsync((int)we.Id);
                    if (work.CandidateId != candidate.Id)
                    {
                        continue;
                    }
                    work.JobTitle = we.JobTitle;
                    work.Organization = we.Organization;
                    work.City = we.City;
                    work.Country = we.Country;
                    work.FromTime = we.FromTime;
                    work.ToTime = we.ToTime;
                    work.Description = we.Description;
                    work.CandidateId = candidate.Id;                    
                    await works.UpdateWorkExperienceAsync(work);
                } 
                else
                {
                    WorkExperience work = new WorkExperience
                    {
                        JobTitle = we.JobTitle,
                        Organization = we.Organization,
                        City = we.City,
                        Country = we.Country,
                        FromTime = we.FromTime,
                        ToTime = we.ToTime,
                        Description = we.Description,
                        CandidateId = candidate.Id
                    };
                    await works.AddWorkExperienceAsync(work);
                }
            }

            await candidates.SaveChangesAsync();
            return candidate.AsDto();
        }
        [HttpPost("skills")]
        [Authorize]
        public async Task<ActionResult<CandidateDto>> UpdateSkills(List<UpdateSkillDto> updateSkillList)
        {

            User user = await GetCurrentUser();
            Candidate candidate = user.Candidate;

            foreach (var sk in updateSkillList)
            {
                if (sk.Id > 0)
                {
                    Skill skill = await skills.GetSkillByIdAsync((int) sk.Id);
                    if (skill.CandidateId != candidate.Id)
                    {
                        continue;
                    }
                    skill.Name = sk.Name;                                       
                    await skills.UpdateSkillAsync(skill);
                }
                else
                {
                    Skill skill = new Skill
                    {                        
                        Name = sk.Name,
                        CandidateId = candidate.Id
                    };
                    await skills.AddSkillAsync(skill);
                }
            }

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
