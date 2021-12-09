using Devjobs.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Devjobs.Repositories
{
    public class CorporatesRepository : ICorporatesRepository
    {
        private DatabaseContext context;

        public CorporatesRepository(DatabaseContext context)
        {
            this.context = context;
        }
        public async Task AddCorporateAsync(Corporate corporate)
        {
            await context.Corporates.AddAsync(corporate);
        }

        public async Task DeleteCorporateAsync(int id)
        {
            Corporate corporate = await context.Corporates.FindAsync(id);
            context.Corporates.Remove(corporate);
        }

        public async Task<Corporate> GetCorporateByIdAsync(int id)
        {
            return await context.Corporates.FindAsync(id);
        }

        public async Task<IEnumerable<Corporate>> GetCorporatesAsync()
        {
            return await context.Corporates.ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }

        public async Task UpdateCorporateAsync(Corporate corporate)
        {
            var corporateInDb = await context.Corporates.FindAsync(corporate.Id);
            corporateInDb = corporate;
            await SaveChangesAsync();
        }
    }
}
