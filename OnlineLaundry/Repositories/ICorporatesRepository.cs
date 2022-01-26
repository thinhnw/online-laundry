using OnlineLaundry.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineLaundry.Repositories
{
    public interface ICorporatesRepository
    {
        Task<IEnumerable<Corporate>> GetCorporatesAsync();
        Task<Corporate> GetCorporateByIdAsync(int id);
        Task<Corporate> AddCorporateAsync(Corporate corporate);
        Task DeleteCorporateAsync(int id);
        Task UpdateCorporateAsync(Corporate corporate);
        Task SaveChangesAsync();
    }
}
