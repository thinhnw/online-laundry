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
        public async Task<Corporate> AddCorporateAsync(Corporate corporate)
        {
            var user = await context.Users.FindAsync(corporate.UserId);
            if (user is null) return null;
            await context.Corporates.AddAsync(corporate);
            await SaveChangesAsync();
            return corporate;
        }

        public async Task DeleteCorporateAsync(int id)
        {
            Corporate corporate = await context.Corporates.FindAsync(id);
            if (corporate != null)
            {
                context.Corporates.Remove(corporate);
                await SaveChangesAsync();
            }
            
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
            context.Entry(corporate).State = EntityState.Modified;
            await SaveChangesAsync();
        }
    }
}
