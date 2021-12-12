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
        private readonly DatabaseContext context;
        private readonly IJobsRepository repository;

        public JobsController(IJobsRepository repository, DatabaseContext context)
        {
            this.context = context;
            this.repository = repository;
        }

        // GET: api/Jobs
        [HttpGet]
        public async Task<IEnumerable<Job>> GetJobs()
        {
            return await repository.GetJobsAsync();
        }

        // GET: api/Jobs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Job>> GetJob(int id)
        {
            var job = await repository.GetJobByIdAsync(id);

            if (job == null)
            {
                return NotFound();
            }

            return job;
        }

        // PUT: api/Jobs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJob(int id, Job job)
        {
            if (id != job.Id)
            {
                return BadRequest();
            }

            context.Entry(job).State = EntityState.Modified;

            try
            {
                await repository.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobExists(id))
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
            await repository.SaveChangesAsync();

            return CreatedAtAction("GetJob", new { id = job.Id }, job);
        }

        // DELETE: api/Jobs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJob(int id)
        {
            var job = await context.Jobs.FindAsync(id);
            if (job == null)
            {
                return NotFound();
            }

            await repository.DeleteJobAsync(id);
            await repository.SaveChangesAsync();

            return NoContent();
        }

        private bool JobExists(int id)
        {
            return context.Jobs.Any(e => e.Id == id);
        }
    }
}
