using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Devjobs.Dtos
{
    public class JobDto
    {
        public string Title { get; init; }
        public string Description { get; init; }
        public string Location { get; init; }
        public int SalaryMin { get; init; }
        public int SalaryMax { get; init; }
        public string Status { get; init; }
        //FK
        public int CorporateId { get; init; }
    }
}
