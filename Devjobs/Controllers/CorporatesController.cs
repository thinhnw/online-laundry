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
    public class CorporatesController : ControllerBase
    {
        private readonly DatabaseContext context;
        private readonly ICorporatesRepository repository;

        public CorporatesController(ICorporatesRepository repository, DatabaseContext context)
        {
            this.context = context;
            this.repository = repository;
        }


        // GET: api/Corporates
        [HttpGet]
        public async Task<IEnumerable<Corporate>> GetCorporates()
        {
            return await repository.GetCorporatesAsync();
        }

        // GET: api/Corporates/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Corporate>> GetCorporate(int id)
        {
            var corporate = await repository.GetCorporateByIdAsync(id);

            if (corporate == null)
            {
                return NotFound();
            }

            return corporate;
        }

        // PUT: api/Corporates/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCorporate(int id, Corporate corporate)
        {
            if (id != corporate.Id)
            {
                return BadRequest();
            }

            context.Entry(corporate).State = EntityState.Modified;

            try
            {
                await repository.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CorporateExists(id))
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

        // POST: api/Corporates
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Corporate>> PostCorporate(CorporateDto corporateDto)
        {
            Corporate corporate = new()
            {
                About = corporateDto.About,
                UserId = corporateDto.UserId
            };
            await repository.AddCorporateAsync(corporate);
            await repository.SaveChangesAsync();

            return CreatedAtAction("GetCorporate", new { id = corporate.Id }, corporate);
        }

        // DELETE: api/Corporates/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCorporate(int id)
        {
            var corporate = await context.Corporates.FindAsync(id);
            if (corporate == null)
            {
                return NotFound();
            }

            await repository.DeleteCorporateAsync(id);
            await repository.SaveChangesAsync();

            return NoContent();
        }

        private bool CorporateExists(int id)
        {
            return context.Corporates.Any(e => e.Id == id);
        }
    }
}
