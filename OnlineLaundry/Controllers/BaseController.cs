using OnlineLaundry.Models;
using OnlineLaundry.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OnlineLaundry.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        protected IUsersRepository users;
        public BaseController(IUsersRepository usersRepo)
        {
            this.users = usersRepo;
        }
        [Authorize]
        protected async Task<User> GetCurrentUser()
        {
            var id = int.Parse(User.Claims.First(c => c.Type == "Id").Value);
            var user = await users.GetUserByIdAsync(id);
            return user;
        }
    }
}
