using Devjobs.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Devjobs.Repositories
{
    public interface IUsersRepository
    {
        Task AddUserAsync(User User);
        Task DeleteUserAsync(int id);
        Task<User> GetUserByIdAsync(int id);
        Task<IEnumerable<User>> GetUsersAsync();
        Task SaveChangesAsync();
        Task UpdateUserAsync(User User);
    }
}