using OnlineLaundry.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineLaundry.Repositories
{
    public interface IUsersRepository
    {
        Task AddUserAsync(User User);
        Task DeleteUserAsync(int id);
        Task<User> GetUserByIdAsync(int id);
        Task<User> GetUserByEmailAsync(string email);
        Task<IEnumerable<User>> GetUsersAsync();
        Task SaveChangesAsync();
        Task UpdateUserAsync(User User);
    }
}