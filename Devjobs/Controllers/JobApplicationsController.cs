using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Devjobs.Models;
using Devjobs.Repositories;

namespace Devjobs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobApplicationsController : ControllerBase
    {
        private readonly DatabaseContext context;
        private readonly IJobApplicationsRepository repository;

        public JobApplicationsController(IJobApplicationsRepository repository, DatabaseContext context)
        {
            this.context = context;
            this.repository = repository;
        }
        // GET: api/JobApplications
        [HttpGet]
        public async Task<IEnumerable<JobApplication>> GetJobApplications()
        {
            return await repository.GetJobApplicationsAsync();
        }

        // GET: api/JobApplications/5
        [HttpGet("{id}")]
        public async Task<ActionResult<JobApplication>> GetJobApplication(int id)
        {
            var jobApplication = await repository.GetJobApplicationByIdAsync(id);

            if (jobApplication == null)
            {
                return NotFound();
            }

            return jobApplication;
        }

        // PUT: api/JobApplications/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJobApplication(int id, JobApplication jobApplication)
        {
            if (id != jobApplication.Id)
            {
                return BadRequest();
            }

            context.Entry(jobApplication).State = EntityState.Modified;

            try
            {
                await repository.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobApplicationExists(id))
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

        // POST: api/JobApplications
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<JobApplication>> PostJobApplication(JobApplication jobApplicationDto)
        {
            JobApplication jobApplication = new()
            {
                Status = jobApplicationDto.Status,
                CV = jobApplicationDto.CV,
                CandidateId = jobApplicationDto.CandidateId,
                JobId = jobApplicationDto.JobId

            };
            await repository.AddJobApplicationAsync(jobApplication);
            await repository.SaveChangesAsync();

            return CreatedAtAction("GetJobApplication", new { id = jobApplication.Id }, jobApplication);
        }

        // DELETE: api/JobApplications/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJobApplication(int id)
        {
            var jobApplication = await context.JobApplications.FindAsync(id);
            if (jobApplication == null)
            {
                return NotFound();
            }

            await repository.DeleteJobApplicationAsync(id);
            await context.SaveChangesAsync();

            return NoContent();
        }

        private bool JobApplicationExists(int id)
        {
            return context.JobApplications.Any(e => e.Id == id);
        }
    }
}
 