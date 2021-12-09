using Devjobs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Devjobs.Repositories
{
    public interface ICorporatesRepository
    {
        Task<IEnumerable<Corporate>> GetCorporatesAsync();
        Task<Corporate> GetCorporateByIdAsync(int id);
        Task AddCorporateAsync(Corporate corporate);
        Task DeleteCorporateAsync(int id);
        Task UpdateCorporateAsync(Corporate corporate);
        Task SaveChangesAsync();
    }
}
