using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineLaundry.Models;

namespace OnlineLaundry.Repositories
{
    public class CategoriesRepository : ICategoriesRepository, IDisposable
    {
        private DatabaseContext context;      

        public CategoriesRepository(DatabaseContext context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await context.Categories.ToListAsync();
        }
        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            return await context.Categories.FindAsync(id);
        }

        public async Task AddCategoryAsync(Category category)
        {
            await context.Categories.AddAsync(category);            
        }

        public async Task DeleteCategoryAsync(int id)
        {
            Category ctg = await context.Categories.FindAsync(id);
            context.Categories.Remove(ctg);
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            var categoryInDb = await context.Categories.FindAsync(category.Id);
            categoryInDb = category;
            await SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
