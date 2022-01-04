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
    public class CorporatesController : BaseController
    {
        private readonly ICorporatesRepository corporates;

        public CorporatesController(ICorporatesRepository corporates, IUsersRepository users) : base(users)
        {            
            this.corporates = corporates;
        }


        // GET: api/Corporates
        [HttpGet]
        public async Task<IEnumerable<CorporateDto>> GetCorporates()
        {
            return (await corporates.GetCorporatesAsync()).Select(corporate => corporate.AsDto());
        }

        // GET: api/Corporates/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CorporateDto>> GetCorporateAsync(int id)
        {
            var corporate = await corporates.GetCorporateByIdAsync(id);

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
                UserId = dto.UserId,
                Logo=dto.Logo,
            };
            var result = await corporates.AddCorporateAsync(corporate);
            if (result is null)
            {
                return NotFound("User not found");
            }
            
            return CreatedAtAction("GetCorporateAsync", new { id = corporate.Id }, corporate);
        }


        // Patch: api/Corporates/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPatch("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateCorporate(int id, UpdateCorporateDto dto)
        {
            var corp = await corporates.GetCorporateByIdAsync(id);
            if (corp is null)
            {
                return NotFound();
            }
            User user = await GetCurrentUser();
            if (!user.Corporate.Equals(corp))
            {
                return Forbid();
            }

            corp.Name = dto.Name;
            corp.Country = dto.Country;
            corp.About = dto.About;
            //corp.Logo = dto.Logo;
            corp.Phone = dto.Phone;
            await corporates.SaveChangesAsync();            

            return NoContent();
        }

       
        // DELETE: api/Corporates/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCorporate(int id)
        {            
            await corporates.DeleteCorporateAsync(id);         

            return NoContent();
        }   
    }
}
