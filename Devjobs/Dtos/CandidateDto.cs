using Devjobs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Devjobs.Dtos
{
    public class CandidateDto
    {
        public string Education { get; init; }
        public int YearsOfExperience { get; init; }
        public decimal CV { get; init; }
        public int UserId { get; init; }
    }
}
