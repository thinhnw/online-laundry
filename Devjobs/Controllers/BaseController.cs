using Devjobs.Models;
using Devjobs.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Devjobs.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        [Authorize]
        protected int GetUserId()
        {
            var test = User.Claims.First(c => c.Type == "Id").Value;
            return int.Parse(this.User.Claims.First(c => c.Type == "Id").Value);
        }
    }
}
