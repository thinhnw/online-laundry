using Devjobs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Devjobs.Dtos
{
    public class JobApplicationDto
    {
        public int Id { get; init; }
        public string Status { get; init; }
        public string CV { get; init; }

        //FK
        public int CandidateId { get; init; }
        public int JobId { get; init; }
    }
}
