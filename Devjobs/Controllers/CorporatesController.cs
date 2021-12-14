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
        private readonly ICorporatesRepository repository;

        public CorporatesController(ICorporatesRepository repository)
        {            
            this.repository = repository;
        }


        // GET: api/Corporates
        [HttpGet]
        public async Task<IEnumerable<CorporateDto>> GetCorporates()
        {
            return (await repository.GetCorporatesAsync()).Select(corporate => corporate.AsDto());
        }

        // GET: api/Corporates/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CorporateDto>> GetCorporateAsync(int id)
        {
            var corporate = await repository.GetCorporateByIdAsync(id);

            if (corporate is null)
            {
                return NotFound();
            }

            return corporate.AsDto();
        }

        // POST: api/Corporates
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Corporate>> PostCorporate(CreateCorporateDto dto)
        {

            Corporate corporate = new()
            {
                Name = dto.Name,
                About = dto.About,
                UserId = dto.UserId
            };
            var result = await repository.AddCorporateAsync(corporate);
            if (result is null)
            {
                return NotFound("User not found");
            }
            
            return CreatedAtAction("GetCorporateAsync", new { id = corporate.Id }, corporate);
        }


        // PUT: api/Corporates/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCorporate(int id, UpdateCorporateDto dto)
        {
            var inDb = await repository.GetCorporateByIdAsync(id);
            if (inDb is null)
            {
                return NotFound();
            }
            inDb.About = dto.About;
            inDb.Name = dto.Name;
            await repository.SaveChangesAsync();            

            return NoContent();
        }

       
        // DELETE: api/Corporates/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCorporate(int id)
        {            
            await repository.DeleteCorporateAsync(id);         

            return NoContent();
        }   
    }
}
