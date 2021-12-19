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
    public class JobApplicationsController : ControllerBase
    {
        private readonly IJobApplicationsRepository repository;

        public JobApplicationsController(IJobApplicationsRepository repository)
        {
            this.repository = repository;
        }
        // GET: api/JobApplications
        [HttpGet]
        public async Task<IEnumerable<JobApplicationDto>> GetJobApplications()
        {
            return (await repository.GetJobApplicationsAsync()).Select(item=>item.AsDto());
        }

        // GET: api/JobApplications/5
        [HttpGet("{id}")]
        public async Task<ActionResult<JobApplicationDto>> GetJobApplication(int id)
        {
            var jobApplication = await repository.GetJobApplicationByIdAsync(id);

            if (jobApplication == null)
            {
                return NotFound();
            }

            return jobApplication.AsDto();
        }

        // PUT: api/JobApplications/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJobApplication(int id, JobApplicationDto jobApplicationDto)
        {
            var inDb = await repository.GetJobApplicationByIdAsync(id);
            if (inDb is null)
            {
                return NotFound();
            }
            JobApplication jobApplication = inDb with
            {
                CV=jobApplicationDto.CV,
                Status=jobApplicationDto.Status,
                CandidateId = jobApplicationDto.CandidateId,
                JobId = jobApplicationDto.JobId
            };
            await repository.UpdateJobApplicationAsync(jobApplication);
            return NoContent();
        }

        // POST: api/JobApplications
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<JobApplication>> PostJobApplication(JobApplicationDto jobApplicationDto)
        {
            JobApplication jobApplication = new()
            {
                Status = jobApplicationDto.Status,
                CV = jobApplicationDto.CV,
                CandidateId = jobApplicationDto.CandidateId,
                JobId = jobApplicationDto.JobId
            };
            await repository.AddJobApplicationAsync(jobApplication);
            return CreatedAtAction("GetJobApplication", new { id = jobApplication.Id }, jobApplication);
        }

        // DELETE: api/JobApplications/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJobApplication(int id)
        {
            var jobApplication = await repository.GetJobApplicationByIdAsync(id);
            if (jobApplication == null)
            {
                return NotFound();
            }

            await repository.DeleteJobApplicationAsync(id);
            return NoContent();
        }

       
    }
}
 