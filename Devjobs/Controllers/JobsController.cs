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
    public class JobsController : ControllerBase
    {
        private readonly IJobsRepository repository;

        public JobsController(IJobsRepository repository)
        {
            this.repository = repository;
        }

        // GET: api/Jobs
        [HttpGet]
        public async Task<IEnumerable<JobDto>> GetJobs()
        {
            return (await repository.GetJobsAsync()).Select(item=>item.AsDto());
        }

        // GET: api/Jobs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<JobDto>> GetJob(int id)
        {
            var job = await repository.GetJobByIdAsync(id);

            if (job == null)
            {
                return NotFound();
            }

            return job.AsDto();
        }

        // PUT: api/Jobs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJob(int id, JobDto jobDto)
        {
            var jobInDb = await repository.GetJobByIdAsync(id);
            if (jobInDb is null)
            {
                return NotFound();
            }
            Job job = jobInDb with
            {
                CorporateId = jobDto.CorporateId,
                Description = jobDto.Description,
                Location = jobDto.Location,
                SalaryMax = jobDto.SalaryMax,
                SalaryMin = jobDto.SalaryMin,
                Status = jobDto.Status,
                Title = jobDto.Title,
            };
            await repository.UpdateJobAsync(job);
            return NoContent();
        }

        // POST: api/Jobs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Job>> PostJob(JobDto jobDto)
        {
            Job job = new()
            {
                Title = jobDto.Title,
                Description = jobDto.Description,
                Location = jobDto.Location,
                SalaryMin = jobDto.SalaryMin,
                SalaryMax = jobDto.SalaryMax,
                Status = jobDto.Status,
                CorporateId = jobDto.CorporateId,
            };
            await repository.AddJobAsync(job);
            return CreatedAtAction("GetJob", new { id = job.Id }, job);
        }

        // DELETE: api/Jobs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJob(int id)
        {
            var job = await repository.GetJobByIdAsync(id);
            if (job == null)
            {
                return NotFound();
            }

            await repository.DeleteJobAsync(id);
            return NoContent();
        }
    }

}
