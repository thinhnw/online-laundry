using Devjobs.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Devjobs.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private DatabaseContext context;

        public UsersRepository(DatabaseContext context)
        {
            this.context = context;
        }
        public async Task AddUserAsync(User User)
        {
            await context.Users.AddAsync(User);
            await SaveChangesAsync();
        }

        public async Task DeleteUserAsync(int id)
        {
            User User = await context.Users.FindAsync(id);
            if (User != null)
            {
                context.Users.Remove(User);
                await SaveChangesAsync();
            }

        }

        public async Task<User> GetUserByEmailAsync(string email)
        {            
            return await context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            var user = await context.Users.Include(u => u.Candidate).Include(u => u.Corporate).FirstOrDefaultAsync(u => u.Id == id);
            return user;
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await context.Users.ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(User User)
        {
            context.Entry(User).State = EntityState.Modified;
            await SaveChangesAsync();
        }
    }
}
