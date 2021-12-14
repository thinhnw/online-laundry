using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Devjobs.Dtos
{
    public class WorkExperienceDto
    {
        public string JobTitle { get; init; }
        public string Organization { get; init; }
        public string City { get; init; }
        public string Country { get; init; }
        public DateTime FromTime { get; init; }
        public DateTime ToTime { get; init; }
        public string Description { get; init; }

        //FK
        public int CandidateId { get; init; }
    }
}
