using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Devjobs.Models;
using Devjobs.Repositories;

namespace Devjobs.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]    
    public class CategoriesController : ControllerBase
    {
        private ICategoriesRepository repository;

        public CategoriesController(ICategoriesRepository repository)
        {
            this.repository = repository;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await repository.GetCategoriesAsync();
        }

        
    }
}
