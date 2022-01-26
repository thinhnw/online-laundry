using OnlineLaundry.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineLaundry.Repositories
{
    public interface IEducationsRepository
    {
        Task<IEnumerable<Education>> GetEducationsAsync();
        Task<Education> GetEducationByIdAsync(int id);
        Task<Education> AddEducationAsync(Education education);
        Task DeleteEducationAsync(int id);
        Task UpdateEducationAsync(Education education);
        Task SaveChangesAsync();
    }
}
