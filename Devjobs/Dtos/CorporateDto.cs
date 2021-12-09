using Devjobs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Devjobs.Dtos
{
    public class CorporateDto
    {
        public int Id { get; init; }
        public string About { get; init; }
        public int UserId { get; init; }
        public virtual User User { get; init; }
    }
}
